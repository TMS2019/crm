using CRMFocus.Business.Interfaces;
using CRMFocus.Entity;
using System.Collections.Generic;
using CRMFocus.DataAccess.Interfaces;
using System.Linq;
using CRMFocus.Domain;
using System;
using System.Web;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using System.Transactions;
using System.Data.Entity.Core;
using CRMFocus.Common;

namespace CRMFocus.Business.Implementations
{
    public class ManageSendSmsService : IManageSendSmsService
    {
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IScenarioLeadMappingRepository _scenarioLeadMappingRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionTemporaryRepository _leadsUnitTransactionTemporaryRepository;

        public ManageSendSmsService(IScenarioRepository scenarioRepository, IScenarioLeadMappingRepository scenarioLeadMappingRepository,
            IDealerRepository dealerRepository, ILeadsRepository leadsRepository, ILeadsUnitTransactionTemporaryRepository leadsUnitTransactionTemporaryRepository)
        {
            _scenarioRepository = scenarioRepository;
            _scenarioLeadMappingRepository = scenarioLeadMappingRepository;
            _dealerRepository = dealerRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionTemporaryRepository = leadsUnitTransactionTemporaryRepository;
        }

        public List<ScenarioLeadMappingView> GetPreviewExcell(HttpFileCollectionBase files, string userRole)
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
            result = ValidateScenarioLeadMapping(result);

            return result;
        }

        public List<Scenario> GetScenarioDropDown()
        {
            return _scenarioRepository.GetAll().ToList();
        }

        public ScenarioLeadMappingView SaveExcell(string[] scenarioLeadMappingViewIds, string userRole, string scenarioCode, string scenarioType)
        {
            var scenarioLeadMappingView = new ScenarioLeadMappingView();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    string[] filePaths = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Temp"));

                    var result = ReadExcellFile(filePaths[0], userRole);

                    for (int i = 0; i < scenarioLeadMappingViewIds.Length; i++)
                    {
                        var leadModel = new Lead();

                        // get 1 scenario mapping from excell
                        var scenarioLeadMappingViewExcell = result.Where(w => w.Telepon == scenarioLeadMappingViewIds[i]).FirstOrDefault();

                        // check if it is exist in the lead table before store it
                        var leadResponse = _leadsRepository.Find(f => f.CellNo == scenarioLeadMappingViewExcell.Telepon && f.Name == scenarioLeadMappingViewExcell.Nama).FirstOrDefault();

                        if (leadResponse != null)
                        {
                            scenarioLeadMappingViewExcell.Status = Utilities.ErrorStatus;
                            scenarioLeadMappingViewExcell.Message = string.Format("Data lead anda dengan Nama: {0} dan Cell No: {1} sudah ada di Database, data tidak bisa di simpan dan akan di rolled back!", leadResponse.Name, leadResponse.CellNo);
                            return scenarioLeadMappingViewExcell;
                        }
                        else
                        {
                            var lead = new Lead()
                            {
                                Address = scenarioLeadMappingViewExcell.Alamat,
                                Name = scenarioLeadMappingViewExcell.Nama,
                                CellNo = scenarioLeadMappingViewExcell.Telepon,
                                SourceSystemCreatedTime = DateTime.Now,
                                SourceSystemLastModifiedTime = DateTime.Now,
                                BirthDate = DateTime.Now
                            };

                            leadModel = _leadsRepository.Create(lead);


                            // generate LeadScenarioMappingCode
                            var LSMCode = "";
                            var scenarioLeadMappingCount = _scenarioLeadMappingRepository.GetAll().Count() + 1;
                            if (scenarioLeadMappingCount < 10)
                            {
                                LSMCode = string.Format("000{0}", scenarioLeadMappingCount);
                            }
                            if (scenarioLeadMappingCount > 9 && scenarioLeadMappingCount < 100)
                            {
                                LSMCode = string.Format("00{0}", scenarioLeadMappingCount);
                            }
                            if (scenarioLeadMappingCount > 99 && scenarioLeadMappingCount < 1000)
                            {
                                LSMCode = string.Format("0{0}", scenarioLeadMappingCount);
                            }
                            if (scenarioLeadMappingCount > 999 && scenarioLeadMappingCount < 10000)
                            {
                                LSMCode = string.Format("{0}", scenarioLeadMappingCount);
                            }
                            // insert into ScenarioLeadsMapping table
                            var scenarioLeadMappingModel = new ScenarioLeadMapping()
                            {
                                LeadScenarioMappingCode = string.Format("LSM{0}{1}", DateTime.Now.Year.ToString().Substring(2, 2), LSMCode),
                                ScenarioCode = scenarioCode,
                                CRMCustomerNum = leadModel.CRMCustomerCode,
                                EngineCode = scenarioLeadMappingViewExcell.EngineCode == null ? null : scenarioLeadMappingViewExcell.EngineCode,
                                EngineNo = scenarioLeadMappingViewExcell.EngineNumber == null ? null : scenarioLeadMappingViewExcell.EngineNumber
                            };

                            var entity = _scenarioLeadMappingRepository.Create(scenarioLeadMappingModel);
                        }

                        // check if EngineCode and EngineNumber is exist in excell
                        if (scenarioLeadMappingViewExcell.EngineCode != null && scenarioLeadMappingViewExcell.EngineNumber != null)
                        {
                            // check if lead unit transaction is exist in temporary table
                            var leadUnitTransactionTemporaryResponse = _leadsUnitTransactionTemporaryRepository.Find(f => f.EngineCode == scenarioLeadMappingViewExcell.EngineCode && f.EngineNo == scenarioLeadMappingViewExcell.EngineNumber).FirstOrDefault();

                            if (leadUnitTransactionTemporaryResponse == null)
                            {
                                var leadUnitTransactionTemporary = new LeadsUnitTransactionTemporary()
                                {
                                    EngineCode = scenarioLeadMappingViewExcell.EngineCode,
                                    EngineNo = scenarioLeadMappingViewExcell.EngineNumber,
                                    LeadsTemporaryId = leadModel.CRMCustomerCode
                                };

                               // var model = _leadsUnitTransactionTemporaryRepository.Create(leadUnitTransactionTemporary);
                            }
                        }
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

            scenarioLeadMappingView.Status = Utilities.SucceedStatus;
            scenarioLeadMappingView.Message = string.Format("{0} data sukses disimpan!", scenarioLeadMappingViewIds.Length);


            return scenarioLeadMappingView;
        }

        private List<ScenarioLeadMappingView> ReadExcellFile(string filePath, string userRole)
        {
            var list = new List<ScenarioLeadMappingView>();
            if (filePath != null)
            {
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(filePath)))
                {
                  //  bool isValid = false;
                    var workSheet = xlPackage.Workbook.Worksheets.First();
                    var totalRows = workSheet.Dimension.End.Row;
                    var totalColumns = workSheet.Dimension.End.Column;



                    //if (isValid)
                    //{
                        var dealers = _dealerRepository.GetAll().ToList();
                        
                        for (int rowIndex = 2; rowIndex <= totalRows; rowIndex++)
                        {
                            var model = MappingToModel(rowIndex, workSheet, userRole, dealers);
                            list.Add(model);
                        }
                    //}

                }
            }

            return list;
        }

        private ScenarioLeadMappingView MappingToModel(int row, ExcelWorksheet workSheet, string userRole, List<Dealer> dealers)
        {
            var model = new ScenarioLeadMappingView();

            if (workSheet.Cells[row, 2].Text == "")
            {
                model.TanggalDiUnggah = null;
            }
            else
            {
                model.TanggalDiUnggah = DateTime.Now; // Tanggal unggah by system
            }

            model.ScenarioLeadMappingViewId = workSheet.Cells[row, 2].Text;
            model.Nama = workSheet.Cells[row, 1].Text; //Nama
            model.Telepon = workSheet.Cells[row, 2].Text; //Telepon
            model.Unit = workSheet.Cells[row, 3].Text; //Unit
            model.Email = workSheet.Cells[row, 4].Text; //Email
            model.Varian = workSheet.Cells[row, 5].Text; //Varian
            model.Alamat = workSheet.Cells[row, 6].Text; //Alamat
            model.Cabang = workSheet.Cells[row, 7].Text; //Cabang
            model.CabangDescription = dealers.Where(w => w.DealerCode == workSheet.Cells[row, 7].Text).Select(s => s.DealerName).FirstOrDefault(); //CabangDescription
            model.EngineCode = workSheet.Cells[row, 8].Text; //EngineCode
            model.EngineNumber = workSheet.Cells[row, 9].Text; //EngineNumber

            return model;
        }

        private List<ScenarioLeadMappingView> ValidateScenarioLeadMapping(List<ScenarioLeadMappingView> scenarioLeadMappingViews)
        {
            var row = 0;
            var page = 1;
            var result = new List<ScenarioLeadMappingView>();
            var leadsRequestList = _leadsRepository.GetAll();
            var leadsUnitTransactionTemporaryRequestList = _leadsUnitTransactionTemporaryRepository.GetAll();
            var scenarioLeadMappingRequestList = _scenarioLeadMappingRepository.GetAll();

            if (scenarioLeadMappingViews.Count() == 1 && (scenarioLeadMappingViews.FirstOrDefault().Nama == null || scenarioLeadMappingViews.FirstOrDefault().Telepon == null))
            {
                return scenarioLeadMappingViews;
            }

            foreach (var item in scenarioLeadMappingViews)
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

                if (item.Nama == "" || item.Telepon == "")
                {
                    item.Status = "Error";
                    item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, kolom Nama dan Cell No tidak boleh kosong!", page, row);
                    result.Add(item);
                    continue;
                }

                var lead = leadsRequestList.Where(w => w.Name == item.Nama && w.CellNo == item.Telepon).FirstOrDefault();
                if (lead != null)
                {
                    item.Status = "Error";
                    item.Message = string.Format("Halaman ke-{0}, Baris ke-{1}, Lead dengan Nama: {2} dan Cell No: {3} sudah ada di Database!", page, row, item.Nama, item.Telepon);
                    result.Add(item);
                    continue;
                }
            }

            return result;
        }
    }
}
