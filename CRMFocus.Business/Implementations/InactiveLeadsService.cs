using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.Entity.Core;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Entity;

namespace CRMFocus.Business.Implementations
{
    public class InactiveLeadsService : IInactiveLeadsService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly ISuspectRepository _suspectRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IMasterStatusRepository _masterStatusRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly ISuspectFollowUpRepository _suspectFollowUpRepository;

        public InactiveLeadsService(ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            ISuspectRepository suspectRepository, IScenarioRepository scenarioRepository,
            IMasterStatusRepository masterStatusRepository, IDealerRepository dealerRepository,
            ISuspectFollowUpRepository suspectFollowUpRepository)
        {
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _suspectRepository = suspectRepository;
            _scenarioRepository = scenarioRepository;
            _masterStatusRepository = masterStatusRepository;
            _dealerRepository = dealerRepository;
            _suspectFollowUpRepository = suspectFollowUpRepository;
        }

        public List<InactiveLeadsView> GetAllInactiveLeads()
        {
            var list = new List<InactiveLeadsView>();
            var suspects = _suspectRepository.Find(f => f.IsInactive == true && f.IsDeleted == false);
            var populateLeads = PopulateLeads();
            var scenarios = _scenarioRepository.GetAll();
            var masterStatuses = _masterStatusRepository.GetAll();
            var dealers = _dealerRepository.GetAll();

            foreach (var item in suspects)
            {
                var lead = populateLeads.Find(f => f.CRMCustomerCode == item.CRMCustomerNum);
                if (lead != null)
                {
                    var inactiveLeadsView = new InactiveLeadsView();
                    inactiveLeadsView.SuspectId = item.SuspectID;
                    inactiveLeadsView.Name = lead.Name;
                    inactiveLeadsView.Telepon = lead.CellNo;
                    inactiveLeadsView.Email = lead.Email;
                    inactiveLeadsView.Kota = lead.Address;
                    inactiveLeadsView.Sumber = lead.LeadsUnitTransactions == null ? string.Empty : lead.LeadsUnitTransactions.Select(s => s.SourceData).FirstOrDefault();
                    inactiveLeadsView.TglMasuk = lead.LeadsUnitTransactions == null ? DateTime.Now : lead.LeadsUnitTransactions.Select(s => s.TglBeliDLR).FirstOrDefault();
                    inactiveLeadsView.Cabang = dealers.Where(w => w.DealerCode == item.CurrentDealer).Select(s => s.DealerName).FirstOrDefault();
                    inactiveLeadsView.Scenario = scenarios.Where(w => w.ScenarioCode == item.ScenarioCode).Select(s => s.ScenarioName).FirstOrDefault();
                    inactiveLeadsView.Stage = masterStatuses.Where(w => w.MasterStatusID == item.SuspectStatus).Select(s => s.Description).FirstOrDefault();
                    list.Add(inactiveLeadsView);
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

        public string ReactivateSuspect(string suspectIds) 
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
                            suspect.IsInactive = false;
                            suspect.SuspectStatus = 10; // 10 == Suspect Status is Low, after reactivated suspect status always LOW
                            suspect.LastReactivate = DateTime.Now;
                            _suspectRepository.Update(suspect);

                            // reset suspect follow up
                            var suspectFollowUps = _suspectFollowUpRepository.Find(f => f.SuspectID == suspect.SuspectID).ToList();
                            foreach (var item in suspectFollowUps)
                            {
                                item.IsDeleted = true;
                                _suspectFollowUpRepository.Update(item);
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

            return "";
        }

        public string DeleteSuspect(string suspectIds)
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
                            suspect.IsDeleted = true;
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
    }
}
