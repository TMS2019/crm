using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMFocus.Entity;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System.Transactions;

namespace CRMFocus.Business.Implementations
{
    public class DistributeSuspectService : IDistributeSuspectService
    {
        private readonly ISuspectRepository _suspectRepository;
        private readonly ISuspectFollowUpRepository _suspectFollowUpRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IEmployeeRepository _employeeRepository;
       // private readonly IEmployeeRepository _ab;

        public DistributeSuspectService(ISuspectRepository suspectRepository,
            ISuspectFollowUpRepository suspectFollowUpRepository,
            ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            IDealerRepository dealerRepository,
            IEmployeeRepository employeeRepository)
        {
            _suspectRepository = suspectRepository;
            _suspectFollowUpRepository = suspectFollowUpRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _dealerRepository = dealerRepository;
            _employeeRepository = employeeRepository;
        }

        public List<Dealer> GetAllDealer()
        {
            return _dealerRepository.Find(d => d.isActive == 1).ToList<Dealer>();
        }
        //test func
        public List<Employee> getSales1(string customerCode)
        {
            return _employeeRepository.Find(s => s.DealerCode == customerCode && s.IsDeleted == false && s.IsSalesActive == 1).ToList<Employee>();
        }

        public List<Employee> GetSalesByDealer(string dealerCode)
        {
            return _employeeRepository.Find(s => s.DealerCode == dealerCode && s.IsDeleted == false && s.IsSalesActive == 1).ToList<Employee>();
        }

        public DistributeSuspectView[] UpdateSuspects(DistributeSuspectView[] updatedSuspects)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                foreach (DistributeSuspectView suspect in updatedSuspects)
                {
                    Suspect s = PopulateSuspect(suspect.SuspectId);

                    //suspect can be null if somehow some one is following up this suspect while it's about to be assigned to someone else
                    if (s != null)
                    {
                        s.CurrentSalesName = suspect.CurrentSales;
                        s.CurrentDealer = suspect.CurrentDealerCode;
                        s.ExpiredDate = DateTime.Now.AddDays(7);

                        _suspectRepository.Update(s);
                    }
                }

                ts.Complete();
            }

                return updatedSuspects;
        }

        public List<DistributeSuspectView> GetSelectedSuspect(string suspectIds)
        {
            List<DistributeSuspectView> distributeSuspectViews = new List<DistributeSuspectView>();
            var dealers = _dealerRepository.GetAll().ToList();

            string[] ids = suspectIds.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                Suspect suspect = PopulateSuspect(ids[i]);
                if (suspect != null)
                {
                    Lead lead = PopulateLead(suspect.CRMCustomerNum);
                    if (lead != null)
                    {
                        DistributeSuspectView view = new DistributeSuspectView();
                        view.SuspectId = suspect.SuspectID;
                        view.Title = lead.Gender == "1" ? "Mr" : "Mrs";
                        view.Name = lead.Name;
                        view.Phone = lead.CellNo;
                        view.LastDealer = suspect.LastDealerName;
                        view.LastSales = suspect.LastSalesName;
                        view.CurrentDealerName = suspect.CurrentDealer == null ? string.Empty : dealers.Where(w => w.DealerCode == suspect.CurrentDealer).Select(s => s.DealerName).FirstOrDefault();
                        view.CurrentDealerCode = suspect.CurrentDealer;
                        view.CurrentSales = suspect.CurrentSalesName;
                        view.LastPurchaseUnit = suspect.LastPurchaseUnit;
                        view.ExpireDate = suspect.ExpiredDate != null ? String.Format("{0:dd-MM-yyyy}", suspect.ExpiredDate) : String.Empty;

                        distributeSuspectViews.Add(view);
                    }
                }
            }

            return distributeSuspectViews;
        }

        private Suspect PopulateSuspect(string suspectId)
        {
            Suspect suspect = _suspectRepository.Find(p => p.SuspectID == suspectId && p.IsInactive == false && p.IsDeleted == false)
                .SingleOrDefault();

            //penjagaan dimana hanya akan mengembalikan suspect yang tidak punya follow-up
            if (suspect != null)
            {
                IEnumerable<SuspectFollowUp> prospectFollowUps;
                if (suspect.LastReactivate == null)
                {
                    prospectFollowUps = _suspectFollowUpRepository.Find(fu => fu.SuspectID == suspect.SuspectID);
                } 
                else
                {
                    prospectFollowUps = _suspectFollowUpRepository.Find(fu => fu.SuspectID == suspect.SuspectID && fu.FollowupDate > suspect.LastReactivate);
                }
                
                if (prospectFollowUps.Count() > 0)
                {
                    suspect = null;
                }
            }

            return suspect;
        }

        private Lead PopulateLead(int crmCustomerCode)
        {
            Lead lead = _leadsRepository.Find(l => l.CRMCustomerCode == crmCustomerCode).FirstOrDefault(); 
            if (lead != null)
            {
                var leadsUnitTransactions = _leadsUnitTransactionRepository.Find(unit => unit.CRMCustomerCode == lead.CRMCustomerCode);
                if (leadsUnitTransactions.Count() > 0)
                {
                    lead.LeadsUnitTransactions = new List<LeadsUnitTransaction>();
                    lead.LeadsUnitTransactions.AddRange(leadsUnitTransactions);
                }
            }
            return lead;
        }
    }
}
