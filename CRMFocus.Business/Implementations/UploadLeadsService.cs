using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System;
using System.Globalization;
using System.Transactions;
using System.Data.Entity.Core;
using AutoMapper;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Entity;

namespace CRMFocus.Business.Implementations
{
    public class UploadLeadsService : IUploadLeadsService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly ILeadsTemporaryRepository _leadsTemporaryRepository;
        private readonly ILeadsUnitTransactionTemporaryRepository _leadsUnitTransactionTemporaryRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IProspectTemporaryRepository _prospectTemporaryRepository;
        private readonly ICustomerProfileRefRepository _customerProfileRefRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IMainDealerRepository _mainDealerRepository;

        public UploadLeadsService(ILeadsTemporaryRepository leadsTemporaryRepository,
            ILeadsUnitTransactionTemporaryRepository leadsUnitTransactionTemporaryRepository,
            ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            IScenarioRepository scenarioRepository,
            IProspectTemporaryRepository prospectTemporaryRepository,
            ICustomerProfileRefRepository customerProfileRefRepository,
            IDealerRepository dealerRepository,
            IMainDealerRepository mainDealerRepository)
        {
            _leadsTemporaryRepository = leadsTemporaryRepository;
            _leadsUnitTransactionTemporaryRepository = leadsUnitTransactionTemporaryRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _scenarioRepository = scenarioRepository;
            _prospectTemporaryRepository = prospectTemporaryRepository;
            _customerProfileRefRepository = customerProfileRefRepository;
            _dealerRepository = dealerRepository;
            _mainDealerRepository = mainDealerRepository;
        }

        public List<UploadLeadsView> GetPreviewExcell(HttpFileCollectionBase files, string userRole)
        {
            string[] filePaths = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Temp"));
            foreach (var item in filePaths)
            {
                File.Delete(item);
            }

            HttpPostedFileBase file = files[0];

            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Temp"), file.FileName);
            file.SaveAs(filePath);

            var result = ReadExcellFile(filePath, userRole);
            result = ValidateUploadLeads(result);

            return result;
        }

        public string SaveExcell(string[] customerCodes, string userRole)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    string[] filePaths = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Temp"));
                    
                    var result = ReadExcellFile(filePaths[0], userRole);                    

                    for (int i = 0; i < customerCodes.Length; i++)
                    {
                        if (customerCodes[i] == "null")
                        {
                            return string.Format("Data tidak bisa disimpan karena ada kolom mandatory yang masih kosong!");
                        }
                            
                        var Ids = customerCodes[i].Split('_');
                        var name = Ids[0];
                        var cellNo = Ids[1];
                        var uploadLeads = result
                            .Where(w => w.Name == name && w.CellNo == cellNo).FirstOrDefault();

                        if (uploadLeads.Name == "" || uploadLeads.CellNo == "" || uploadLeads.SourceData == "")
                        {
                          return string.Format("Data tidak bisa disimpan karena ada kolom mandatory yang masih kosong!");
                        }

                        // if source == ADP then insert to lead, lead unit transaction and Prospect table
                        if (uploadLeads.SourceData.ToLower() == "ADP".ToLower())
                        {
                            var lead = new Lead();
                            Mapper.Map(uploadLeads, lead);
                            var leadUnitTransaction = new LeadsUnitTransaction();
                            Mapper.Map(uploadLeads, leadUnitTransaction);

                            // Default initialize datetime
                            lead.SourceSystemCreatedTime = DateTime.Now;
                            lead.SourceSystemLastModifiedTime = DateTime.Now;
                            leadUnitTransaction.TglBeliDLR = uploadLeads.TglBeli == null ? DateTime.Now : Convert.ToDateTime(uploadLeads.TglBeli); // question! tgl Beli boleh kosong?
                            leadUnitTransaction.SourceSystemCreatedTime = DateTime.Now;
                            leadUnitTransaction.SourceSystemLastModifiedTime = DateTime.Now;


                            // Check if lead is exist in lead table based on Name and Cell Number
                            var leadResponse = _leadsRepository.Find(f => f.Name == lead.Name && f.CellNo == lead.CellNo).FirstOrDefault();
                            if (leadResponse != null)
                            {
                                
                                if (leadResponse != null && leadUnitTransaction.EngineNo == "" && leadUnitTransaction.EngineCode == "")
                                {
                                    return string.Format("Data lead anda dengan Nama: {0} dan Cell No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadResponse.Name, leadResponse.CellNo);
                                }

                                if (leadUnitTransaction.EngineNo != "" && leadUnitTransaction.EngineCode != "")
                                {
                                    // Check if lead unit transaction is exist in lead unit transaction table based on EngineCode and EngineNo
                                    var leadUnitTransactionResponse = _leadsUnitTransactionRepository.Find(f => f.EngineCode == leadUnitTransaction.EngineCode && f.EngineNo == leadUnitTransaction.EngineNo).FirstOrDefault();
                                    if (leadUnitTransactionResponse != null)
                                    {
                                        return string.Format("Data lead unit transaction anda dengan Engine Code: {0} and Engine No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadUnitTransactionResponse.EngineCode, leadUnitTransactionResponse.EngineNo);
                                    }

                                    leadUnitTransaction.CRMCustomerCode = leadResponse.CRMCustomerCode;
                                    _leadsUnitTransactionRepository.Create(leadUnitTransaction);                                    
                                }

                                // Insert into prospect temporary
                                InsertToProspectTemporaryTable(uploadLeads, leadResponse);
                                continue;
                            }

                            // Insert into lead table
                            var leadResult = _leadsRepository.Create(lead);

                            // Insert into prospect temporary
                            InsertToProspectTemporaryTable(uploadLeads, leadResult);

                            // Insert into lead unit transaction table
                            if (leadUnitTransaction.EngineNo != "" || leadUnitTransaction.EngineCode != "")
                            {
                                // Check if lead unit transaction is exist in lead unit transaction table based on EngineCode and EngineNo
                                var leadUnitTransactionResponse = _leadsUnitTransactionRepository.Find(f => f.EngineCode == leadUnitTransaction.EngineCode && f.EngineNo == leadUnitTransaction.EngineNo).FirstOrDefault();
                                if (leadUnitTransactionResponse != null)
                                {
                                    return string.Format("Data lead unit transaction anda dengan Engine Code: {0} and Engine No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadUnitTransactionResponse.EngineCode, leadUnitTransactionResponse.EngineNo);
                                }

                                leadUnitTransaction.CRMCustomerCode = leadResult.CRMCustomerCode;
                                _leadsUnitTransactionRepository.Create(leadUnitTransaction);                                
                            }
                            continue;
                        }


                        // lead temporary and lead unit temporary area
                        var leadTemporary = new LeadsTemporary();
                        Mapper.Map(uploadLeads, leadTemporary);
                        var leadUnitTransactionTemporary = new LeadsUnitTransactionTemporary();
                        Mapper.Map(uploadLeads, leadUnitTransactionTemporary);

                        // Check if lead is exist in temporary table based on Name and Cell Number
                        var leadTemporaryResponse = _leadsTemporaryRepository.Find(f => f.Name == leadTemporary.Name && f.CellNo == leadTemporary.CellNo).FirstOrDefault();
                        if (leadTemporaryResponse != null)
                        {
                            if (leadTemporaryResponse != null && leadUnitTransactionTemporary.EngineNo == "" && leadUnitTransactionTemporary.EngineCode == "")
                            {
                                return string.Format("Data lead anda dengan Nama: {0} dan Cell No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadTemporaryResponse.Name, leadTemporaryResponse.CellNo);
                            }

                            if (leadUnitTransactionTemporary.EngineNo != "" || leadUnitTransactionTemporary.EngineCode != "")
                            {
                                // Check if lead unit transaction is exist in temporary table based on EngineCode and EngineNo
                                var leadUnitTransactionTemporaryResponse = _leadsUnitTransactionTemporaryRepository.Find(f => f.EngineCode == leadUnitTransactionTemporary.EngineCode && f.EngineNo == leadUnitTransactionTemporary.EngineNo).FirstOrDefault();
                                if (leadUnitTransactionTemporaryResponse != null)
                                {
                                    return string.Format("Data lead unit transaction anda dengan Engine Code: {0} and Engine No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadUnitTransactionTemporaryResponse.EngineCode, leadUnitTransactionTemporaryResponse.EngineNo);
                                }

                                leadUnitTransactionTemporary.LeadsTemporaryId = leadTemporaryResponse.Id;
                                _leadsUnitTransactionTemporaryRepository.Create(leadUnitTransactionTemporary);
                            }

                            continue;
                        }

                        // Insert into lead temporary table
                        var leadTemoporaryResult = _leadsTemporaryRepository.Create(leadTemporary);

                        // Insert into lead unit transaction temporary table
                        if (leadUnitTransactionTemporary.EngineNo != "" || leadUnitTransactionTemporary.EngineCode != "")
                        {
                            // Check if lead unit transaction is exist in temporary table based on EngineCode and EngineNo
                            var leadUnitTransactionTemporaryResponse = _leadsUnitTransactionTemporaryRepository.Find(f => f.EngineCode == leadUnitTransactionTemporary.EngineCode && f.EngineNo == leadUnitTransactionTemporary.EngineNo).FirstOrDefault();
                            if (leadUnitTransactionTemporaryResponse != null)
                            {
                                return string.Format("Data lead unit transaction anda dengan Engine Code: {0} and Engine No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadUnitTransactionTemporaryResponse.EngineCode, leadUnitTransactionTemporaryResponse.EngineNo);
                            }
                           
                            leadUnitTransactionTemporary.LeadsTemporaryId = leadTemoporaryResult.Id;
                            _leadsUnitTransactionTemporaryRepository.Create(leadUnitTransactionTemporary);                            
                        }
                        continue;
                    }
                                  
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return string.Format("{0} data sukses disimpan!", customerCodes.Length);
        }

        private void InsertToProspectTemporaryTable(UploadLeadsView uploadLeads, Lead lead)
        {
            // insert into prospect temporary
            var prospect = new ProspectTemporary();
            prospect.CRMCustomerNum = lead.CRMCustomerCode;
            prospect.ScenarioCode = _scenarioRepository.Find(f => f.isCall == 0 & f.isSMS == 0 & (f.StartDate < DateTime.Now & f.EndDate > DateTime.Now)).Select(s => s.ScenarioCode).FirstOrDefault();
            prospect.CurrentDealer = uploadLeads.DealerCode;
            prospect.CurrentSalesNo = "";
            prospect.CurrentSalesName = "";
            prospect.ProspectStatus = 1;
            prospect.IsActive = 1;

            _prospectTemporaryRepository.Create(prospect);
        }

        private List<UploadLeadsView> ReadExcellFile(string filePath, string userRole)
        {
            var list = new List<UploadLeadsView>();
            if (filePath != null)
            {
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filePath)))
                {
                    bool isValid = false;
                    var workSheet = xlPackage.Workbook.Worksheets.First();
                    var totalRows = workSheet.Dimension.End.Row;
                    var totalColumns = workSheet.Dimension.End.Column;
                    var customerProfileRefs = _customerProfileRefRepository.GetAll().ToList();
                    var dealers = _dealerRepository.GetAll().ToList();
                    var mainDealers = _mainDealerRepository.GetAll().ToList();

                    if (userRole.ToLower() == "Dealer".ToLower())
                    {
                        if (totalColumns != 26)
                        {
                            var model = new UploadLeadsView() { Message = "Dokumen anda tidak valid!, tolong cek urutan kolomnya.", Status = "Error" };
                            list.Add(model);
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    else if (userRole.ToLower() == "Main Dealer".ToLower())
                    {
                        if (totalColumns != 28)
                        {
                            var model = new UploadLeadsView() { Message = "Document anda tidak valid!, tolong tambahkan kolom 'Dealer Code' dan 'Dealer Name' serta tolong cek urutan kolomnya.", Status = "Error" };
                            list.Add(model);
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    else if (userRole.ToLower() == "HO".ToLower())
                    {
                        if (totalColumns != 30)
                        {
                            var model = new UploadLeadsView() { Message = "Document anda tidak valid!, tolong tambahkan kolom 'Dealer Code','Dealer Name', 'Region Code' dan 'Region Name' serta tolong cek urutan kolomnya.", Status = "Error" };
                            list.Add(model);
                        }
                        else
                        {
                            isValid = true;
                        }
                    }

                    if (isValid)
                    {
                        for (int rowIndex = 2; rowIndex <= totalRows; rowIndex++)
                        {
                            var model = MappingToModel(rowIndex, workSheet, customerProfileRefs, dealers, mainDealers, userRole);
                            list.Add(model);
                        }
                    }
                   
                }
            }

            return list;
        }

        private UploadLeadsView MappingToModel(int row, ExcelWorksheet workSheet, List<CustomerProfileRef> customerProfileRefs, List<Dealer> dealers, List<MainDealer> mainDealers, string userRole)
        {
            var model = new UploadLeadsView();

            if (workSheet.Cells[row, 4].Text == "" || workSheet.Cells[row, 4].Text.Split('/').Count() == 1)
            {
                model.BirthDate = null;
            }
            else
            {
                model.BirthDate = DateTime.ParseExact(workSheet.Cells[row, 4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //BirthDate
            }

            if (workSheet.Cells[row, 17].Text == "" || workSheet.Cells[row, 17].Text.Split('/').Count() == 1)
            {
                model.TglBeli = null;
            }
            else
            {
                model.TglBeli = DateTime.ParseExact(workSheet.Cells[row, 17].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Tgl Beli
            }

            if (workSheet.Cells[row, 20].Text == "" || workSheet.Cells[row, 20].Text.Split('/').Count() == 1)
            {
                model.ServiceDate = null;
            }
            else
            {
                model.ServiceDate =  DateTime.ParseExact(workSheet.Cells[row, 20].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //Sevice Date
            }

            model.UploadLeadsViewId = workSheet.Cells[row, 1].Text + "_" + workSheet.Cells[row, 2].Text;
            model.Name = workSheet.Cells[row, 1].Text; //Name*
            model.CellNo = workSheet.Cells[row, 2].Text; //CellNo*
            model.CustomerCode = workSheet.Cells[row, 3].Text; //Customer Code
         
            model.Address = workSheet.Cells[row, 5].Text; //Address
            model.Gender = workSheet.Cells[row, 6].Text;
            model.GenderDescription = customerProfileRefs.Where(w => w.Type == "GENDER" && w.Value == workSheet.Cells[row, 6].Text).Select(s => s.Text).FirstOrDefault(); // row[6]; //Gender
            model.Religion = workSheet.Cells[row, 7].Text;
            model.ReligionDescription = customerProfileRefs.Where(w => w.Type == "RELIGION" && w.Value == workSheet.Cells[row, 7].Text).Select(s => s.Text).FirstOrDefault(); //row[7]; //Religion
            model.Profesion = workSheet.Cells[row, 8].Text;
            model.ProfesionDescription = customerProfileRefs.Where(w => w.Type == "JOB" && w.Value == workSheet.Cells[row, 8].Text).Select(s => s.Text).FirstOrDefault(); //row[8];//Profesion
            model.Spending = workSheet.Cells[row, 9].Text;
            model.SpendingDescription = customerProfileRefs.Where(w => w.Type == "MONTHEXPENSE" && w.Value == workSheet.Cells[row, 9].Text).Select(s => s.Text).FirstOrDefault(); // row[9]; //Spending
            model.Education = workSheet.Cells[row, 10].Text;
            model.EducationDescription = customerProfileRefs.Where(w => w.Type == "EDUCATION" && w.Value == workSheet.Cells[row, 10].Text).Select(s => s.Text).FirstOrDefault(); //row[10]; //Education
            model.isCallable = workSheet.Cells[row, 11].Text; //isCallable
            model.Email = workSheet.Cells[row, 12].Text; //Email
            model.SourceData = workSheet.Cells[row, 13].Text; //SourceData*
            model.UnitMarketName = workSheet.Cells[row, 14].Text;//Unit Market Name
            model.EngineCode = workSheet.Cells[row, 15].Text; //EngineCode**
            model.EngineNo = workSheet.Cells[row, 16].Text; //EngineNo**
            
            model.PaymentType = workSheet.Cells[row, 18].Text; //Payment Type
            model.PaymentTypeDescription = customerProfileRefs.Where(w => w.Type == "PAYTYPE" && w.Value == workSheet.Cells[row, 18].Text).Select(s => s.Text).FirstOrDefault(); //Payment Type
            model.ServiceType = workSheet.Cells[row, 19].Text; //Service Type
            
            model.Kelurahan = workSheet.Cells[row, 21].Text; //Kelurahan
            model.Kecamatan = workSheet.Cells[row, 22].Text; //Kecamatan
            model.Kabupaten = workSheet.Cells[row, 23].Text; //Kabupaten
            model.Provinsi = workSheet.Cells[row, 24].Text; //Provinsi
            model.UnitTypeSegment = workSheet.Cells[row, 25].Text; //UnitTypeSegment
            model.UnitTypeSeries = workSheet.Cells[row, 26].Text; //UnitTypeSeries    
               
            if (userRole.ToLower() == "ho")
            {
                model.MainDealerCode = workSheet.Cells[row, 27].Text; // Region Code*
                model.MainDealerName = mainDealers.Where(w => w.MainDealerCode == workSheet.Cells[row, 27].Text).Select(s => s.MainDealerName).FirstOrDefault(); //workSheet.Cells[row, 31].Text; // Region Name*
                model.DealerCode = workSheet.Cells[row, 29].Text; // Dealer Code*
                model.DealerName = dealers.Where(w => w.DealerCode == workSheet.Cells[row, 29].Text).Select(s => s.DealerName).FirstOrDefault(); //workSheet.Cells[row, 29].Text; // Dealer Name*
            }

            if (userRole.ToLower() == "main dealer")
            {
                model.DealerCode = workSheet.Cells[row, 27].Text; // Dealer Code*
                model.DealerName = dealers.Where(w => w.DealerCode == workSheet.Cells[row, 27].Text).Select(s => s.DealerName).FirstOrDefault(); //workSheet.Cells[row, 28].Text; // Dealer Name*
            }
            
         

            return model;
        }

        private List<UploadLeadsView> ValidateUploadLeads(List<UploadLeadsView> uploadLeadsViews)
        {
            var row = 0;
            var page = 1;
            var result = new List<UploadLeadsView>();
            var leadsRequestList = _leadsRepository.GetAll();
            var leadsTemporaryRequestList = _leadsTemporaryRepository.GetAll();
            var leadsUnitTransactionRequestList = _leadsUnitTransactionRepository.GetAll();
            var leadsUnitTransactionTemporaryRequestList = _leadsUnitTransactionTemporaryRepository.GetAll();

            if (uploadLeadsViews.Count() == 1 && uploadLeadsViews.FirstOrDefault().Name == null)
            {
                return uploadLeadsViews;
            }

            foreach (var item in uploadLeadsViews)
            {
                if (row == 10)
                {
                    row = 1;
                    page = page + 1;
                }
                else
                {
                    row = row + 1;
                }

                if (item.Name == "" || item.CellNo == "" || item.SourceData == "")
                {
                    item.Status = "Error";
                    item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, kolom Nama, Cell No, dan Source Data tidak boleh kosong!", page, row);
                    result.Add(item);
                    continue;
                }

              
                if (item.SourceData.ToUpper() == "ADP")
                {
                    var lead = leadsRequestList.Where(w => w.Name == item.Name && w.CellNo == item.CellNo).FirstOrDefault();
                    var leadUnitTransaction = leadsUnitTransactionRequestList.Where(w => w.EngineCode == item.EngineCode && w.EngineNo == item.EngineNo).FirstOrDefault();

                    if (lead != null && leadUnitTransaction != null)
                    {
                        item.Status = "Error";
                        item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, Lead dengan Nama: {2} dan Cell No: {3} serta Lead Unit Transaction dengan Engine Code: {4} dan Engine No: {5} sudah ada di Database!", page, row, item.Name, item.CellNo, item.EngineCode, item.EngineNo);
                        result.Add(item);
                        continue;
                    }

                    var leadUnitTransactionExcell = uploadLeadsViews.Where(w => w.EngineCode == "" && w.EngineNo == "").FirstOrDefault();
                    if (lead != null && leadUnitTransactionExcell != null)
                    {
                        item.Status = "Error";
                        item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, Lead dengan Nama: {2} dan Cell No: {3} sudah ada di Database!", page, row, item.Name, item.CellNo);
                        result.Add(item);
                        continue;
                    }

                    result.Add(item);
                }
                else
                {
                    var leadsTemporary = leadsTemporaryRequestList.Where(w => w.Name == item.Name && w.CellNo == item.CellNo).FirstOrDefault();
                    var leadUnitTransactionTemporary = leadsUnitTransactionTemporaryRequestList.Where(w => w.EngineCode == item.EngineCode && w.EngineNo == item.EngineNo).FirstOrDefault();
                    if (leadsTemporary != null && leadUnitTransactionTemporary != null)
                    {
                        item.Status = "Error";
                        item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, Lead dengan Nama: {2} dan Cell No: {3} serta Lead Unit Transaction dengan Engine Code: {4} dan Engine No: {5} sudah ada di Database!", page, row, item.Name, item.CellNo, item.EngineCode, item.EngineNo);
                        result.Add(item);
                        continue;
                    }

                    var leadUnitTransactionExcell = uploadLeadsViews.Where(w => w.EngineCode == "" && w.EngineNo == "").FirstOrDefault();
                    if (leadsTemporary != null && leadUnitTransactionExcell != null)
                    {
                        item.Status = "Error";
                        item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, Lead dengan Nama: {2} dan Cell No: {3} sudah ada di Database!", page, row, item.Name, item.CellNo);
                        result.Add(item);
                        continue;
                    }

                    result.Add(item);
                }                
            }


            return result;           
        }
    }
}
