using System.Collections.Generic;
using System.Linq;
using System;
using System.Transactions;
using System.Data.Entity.Core;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Entity;

namespace CRMFocus.Business.Implementations
{
    public class ManageLeadsService : IManageLeadsService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly ISuspectRepository _suspectRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly ISuspectFollowUpRepository _suspectFollowUpRepository;

        public ManageLeadsService(ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            ISuspectRepository suspectRepository, IScenarioRepository scenarioRepository, 
            IDealerRepository dealerRepository, ISuspectFollowUpRepository suspectFollowUpRepository)
        {
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _suspectRepository = suspectRepository;
            _scenarioRepository = scenarioRepository;
            _dealerRepository = dealerRepository;
            _suspectFollowUpRepository = suspectFollowUpRepository;
        }

        public List<ManageLeadsView> GetAllLeads()
        {
            var list = new List<ManageLeadsView>();
            var suspects = _suspectRepository.Find(f => f.IsInactive == false && f.IsDeleted == false);
            var populateLeads = PopulateLeads();
            var scenarios = _scenarioRepository.GetAll();
            var dealers = _dealerRepository.GetAll();
            var inactiveDealer = dealers.Where(w => w.isActive == 0).ToList();
            var suspectFollowUps = _suspectFollowUpRepository.GetAll();

            foreach (var item in suspects)
            {
                var isValid = true;
                var isDealerInactive = false;
                var lead = populateLeads.Find(f => f.CRMCustomerCode == item.CRMCustomerNum);
                var suspectFolloUpCount = suspectFollowUps.Where(w => w.SuspectID == item.SuspectID && w.IsDeleted == false).Count();

                if (item.LastReactivate.HasValue)
                {
                    if (item.LastReactivate.Value.Date < DateTime.Now.Date && suspectFolloUpCount > 0)
                    {
                        isValid = false;
                    }
                }

                // Display based on Inactive Dealer
                var isDealerExist = inactiveDealer.Where(w => w.DealerCode == item.CurrentDealer).Count();
                if (isDealerExist > 0) {
                    isDealerInactive = true;
                }

                if (lead != null && isValid && isDealerInactive)
                {
                    var manageLeadsView = new ManageLeadsView();
                    manageLeadsView.SuspectId = item.SuspectID;
                    manageLeadsView.Name = lead.Name;
                    manageLeadsView.Telepon = lead.CellNo;
                    manageLeadsView.Email = lead.Email;
                    manageLeadsView.PembelianTerakhir = item.LastPurchaseUnit;
                    manageLeadsView.Sumber = lead.LeadsUnitTransactions == null ? string.Empty : lead.LeadsUnitTransactions.Select(s => s.SourceData).FirstOrDefault();
                    manageLeadsView.TglMasuk = lead.LeadsUnitTransactions == null ? DateTime.Now : lead.LeadsUnitTransactions.Select(s => s.TglBeliDLR).FirstOrDefault();
                    manageLeadsView.Cabang = dealers.Where(w => w.DealerCode == item.LastDealerName).Select(s => s.DealerName).FirstOrDefault();
                    manageLeadsView.CabangBaru = dealers.Where(w => w.DealerCode == item.CurrentDealer).Select(s => s.DealerName).FirstOrDefault();
                    manageLeadsView.ScenarioName = scenarios.Where(w => w.ScenarioCode == item.ScenarioCode).Select(s => s.ScenarioName).FirstOrDefault();
                    list.Add(manageLeadsView);
                }
            }

            return list;
        }

        private List<Lead> PopulateLeads()
        {
            var list = new List<Lead>();
            var leads = _leadsRepository.GetAll();
            var leadsUnitTransactions = _leadsUnitTransactionRepository.GetAll();

            foreach (var item in leads)
            {
                var result = leadsUnitTransactions.Where(w => w.CRMCustomerCode == item.CRMCustomerCode);
                if (result.Count() > 0)
                {
                    item.LeadsUnitTransactions = new List<LeadsUnitTransaction>();
                    item.LeadsUnitTransactions.AddRange(result);
                }
                list.Add(item);
            }

            return list;
        }

        public string UpdateSuspectDealer(string suspectIds, string currentDealer)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var ids = suspectIds.Split(',');
                    for (int i = 0; i < ids.Length; i++)
                    {
                        var suspectId = ids[i].ToString();
                        var suspect = _suspectRepository.Find(f => f.SuspectID == suspectId).FirstOrDefault();
                        if (suspect != null)
                        {
                            suspect.CurrentDealer = currentDealer;
                            _suspectRepository.Update(suspect);
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

            return "";
        }

        public List<Dealer> GetDealerDropDown()
        {
            return _dealerRepository.GetAll().Where(w => w.isActive == 1).ToList();
        }
    }
}
