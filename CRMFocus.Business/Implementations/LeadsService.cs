using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.Entity.Core;
using CRMFocus.Domain;
using AutoMapper;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.Business.Implementations
{
    public class LeadsService : ILeadsService
    {
        private readonly ILeadsTemporaryRepository _leadsTemporaryRepository;
        private readonly ILeadsUnitTransactionTemporaryRepository _leadsUnitTransactionTemporaryRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly ICustomerProfileRefRepository _customerProfileRefRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IKabupatenRepository _kabupatenRepository;
        private readonly IKecamatanRepository _kecamatanRepository;
        private readonly IKelurahanRepository _kelurahanRepository;
        private readonly IProspectRepository _prospectRepository;
        private readonly IProspectTemporaryRepository _prospectTemporaryRepository;
        private readonly IUnityTypeMarketRepository _unityTypeMarketRepository;

        public LeadsService(ILeadsTemporaryRepository leadsTemporaryRepository,
            ILeadsUnitTransactionTemporaryRepository leadsUnitTransactionTemporaryRepository, 
            ILeadsRepository leadsRepository, ICustomerProfileRefRepository customerProfileRefRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository, IProvinceRepository provinceRepository,
            IKabupatenRepository kabupatenRepository, IKecamatanRepository kecamatanRepository,
            IKelurahanRepository kelurahanRepository, IProspectRepository prospectRepository, 
            IProspectTemporaryRepository prospectTemporaryRepository, 
            IUnityTypeMarketRepository unityTypeMarketRepository)
        {
            _leadsTemporaryRepository = leadsTemporaryRepository;
            _leadsUnitTransactionTemporaryRepository = leadsUnitTransactionTemporaryRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _customerProfileRefRepository = customerProfileRefRepository;
            _provinceRepository = provinceRepository;
            _kabupatenRepository = kabupatenRepository;
            _kecamatanRepository = kecamatanRepository;
            _kelurahanRepository = kelurahanRepository;
            _prospectRepository = prospectRepository;
            _prospectTemporaryRepository = prospectTemporaryRepository;
            _unityTypeMarketRepository = unityTypeMarketRepository;
        }

        public bool Create(CreateLeadsView leadsView)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var leads = new Lead();
                    var leadsTemporary = new LeadsTemporary();

                    if (leadsView.LeadsUnitTransaction != null)
                    {
                        var leadToProspect = leadsView.LeadsUnitTransaction.Where(w => w.SourceData.ToLower() == "adp").ToList();
                        if (leadToProspect.Count() == 0) // store it to temporary
                        {
                            Mapper.Map(leadsView, leadsTemporary);
                            var model = _leadsTemporaryRepository.Create(leadsTemporary);

                            foreach (var item in leadsView.LeadsUnitTransaction)
                            {
                                item.LeadsTemporaryId = model.Id;

                                var leadsUnitTransactionTemporary = new LeadsUnitTransactionTemporary();

                                Mapper.Map(item, leadsUnitTransactionTemporary);
                                _leadsUnitTransactionTemporaryRepository.Create(leadsUnitTransactionTemporary);
                            }
                        }
                        else // store to prospect
                        {
                            Mapper.Map(leadsView, leads);
                            leads.SourceSystemCreatedTime = DateTime.Now;
                            leads.SourceSystemLastModifiedTime = DateTime.Now;

                            var model = _leadsRepository.Create(leads);

                            foreach (var item in leadsView.LeadsUnitTransaction)
                            {
                                var leadsUnitTransaction = new LeadsUnitTransaction();

                                Mapper.Map(item, leadsUnitTransaction);
                                leadsUnitTransaction.CRMCustomerCode = model.CRMCustomerCode;
                                leadsUnitTransaction.SourceSystemCreatedTime = DateTime.Now;
                                leadsUnitTransaction.SourceSystemLastModifiedTime = DateTime.Now;
                                leadsUnitTransaction.TglBeliDLR = DateTime.Now;
                                _leadsUnitTransactionRepository.Create(leadsUnitTransaction);
                            }

                            var prospectTemporary = new ProspectTemporary();
                            prospectTemporary.CRMCustomerNum = model.CRMCustomerCode;
                            prospectTemporary.ProspectStatus = 1;
                            prospectTemporary.IsActive = 1;

                            _prospectTemporaryRepository.Create(prospectTemporary);
                        }
                    }
                    else
                    {
                        Mapper.Map(leadsView, leadsTemporary);
                        var model = _leadsTemporaryRepository.Create(leadsTemporary);
                    }

                    ts.Complete();
                    return true;
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
        }

        public List<CustomerProfileRef> GetAllCustomerProfileRef()
        {
            return _customerProfileRefRepository.GetAll().ToList();
        }

        public List<Province> GetAllProvince()
        {
            return _provinceRepository.GetAll().ToList();
        }

        public List<Kabupaten> GetAlKabupaten()
        {
            return _kabupatenRepository.GetAll().ToList();
        }

        public List<Kecamatan> GetAllKecamatan()
        {
            return _kecamatanRepository.GetAll().ToList();
        }

        public List<Kelurahan> GetAllKelurahan()
        {
            return _kelurahanRepository.GetAll().ToList();
        }
        public List<UnityTypeMarket> GetUnityTypeMarket()
        {
            return _unityTypeMarketRepository.GetAll().ToList();
        }
        public List<LeadsTemporaryView> GetAllLeadsTemporary()
        {
            var list = new List<LeadsTemporaryView>();
            var leads = _leadsTemporaryRepository.GetAll().OrderByDescending(s => s.CreatedTime);
            foreach (var lead in leads)
            {
                var item = _leadsUnitTransactionTemporaryRepository.Find(f => f.LeadsTemporaryId == lead.Id).LastOrDefault();
                var leadsView = new LeadsTemporaryView();
                leadsView.Id = lead.Id;
                leadsView.CustomerCode = GetAllCustomerProfileRef().Where(w => w.Type == "CUSTSALESTYPE" && w.Value == lead.CustomerCode).Select(s => s.Text).FirstOrDefault();
                leadsView.BirthDate = lead.BirthDate;
                leadsView.Address = lead.Address;
                leadsView.Gender = GetAllCustomerProfileRef().Where(w => w.Type == "GENDER" && w.Value == lead.Gender).Select(s => s.Text).FirstOrDefault();
                leadsView.Religion = GetAllCustomerProfileRef().Where(w => w.Type == "RELIGION" && w.Value == lead.Religion).Select(s => s.Text).FirstOrDefault();
                leadsView.Profesion = GetAllCustomerProfileRef().Where(w => w.Type == "JOB" && w.Value == lead.Profesion).Select(s => s.Text).FirstOrDefault();
                leadsView.Spending = GetAllCustomerProfileRef().Where(w => w.Type == "MONTHEXPENSE" && w.Value == lead.Spending).Select(s => s.Text).FirstOrDefault();
                leadsView.Education = GetAllCustomerProfileRef().Where(w => w.Type == "EDUCATION" && w.Value == lead.Education).Select(s => s.Text).FirstOrDefault();
                leadsView.Name = lead.Name;
                leadsView.CellNo = lead.CellNo;
                leadsView.Email = lead.Email;
                if (item != null)
                {
                    leadsView.UnitMarketName = _unityTypeMarketRepository.Find(f => f.UnitTypeCode == item.UnitMarketName).Select(s => s.UnitMarketName).FirstOrDefault();
                    leadsView.EngineCode = item.EngineCode;
                    leadsView.EngineNo = item.EngineNo;
                    leadsView.TglBeli = item.TglBeli;
                    leadsView.PaymentType = GetAllCustomerProfileRef().Where(w => w.Type == "PAYTYPE" && w.Value == item.PaymentType).Select(s => s.Text).FirstOrDefault();
                    leadsView.SourceData = item.SourceData;
                    leadsView.Kelurahan = _kelurahanRepository.Find(f => f.KelurahanCode == item.Kelurahan).Select(s => s.KelurahanName).FirstOrDefault();
                    leadsView.Kecamatan = _kecamatanRepository.Find(f => f.KecamatanCode == item.Kecamatan).Select(s => s.KecamatanName).FirstOrDefault();
                    leadsView.Kabupaten = _kabupatenRepository.Find(f => f.KabupatenCode == item.Kabupaten).Select(s => s.KabupatenName).FirstOrDefault();
                    leadsView.Provinsi = _provinceRepository.Find(f => f.ProvinceCode == item.Provinsi).Select(s => s.ProvinceName).FirstOrDefault();
                    leadsView.UnitTypeSegment = item.UnitTypeSegment;
                    leadsView.UnitTypeSeries = item.UnitTypeSeries;
                }
                list.Add(leadsView);
            }

            return list;
        }
    }
}
