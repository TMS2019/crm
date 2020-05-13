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
    public class DistributeProspectService : IDistributeProspectService
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IProspectFollowUpRepository _prospectFollowUpRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DistributeProspectService(IProspectRepository prospectRepository,
            IProspectFollowUpRepository prospectFollowUpRepository,
            ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            IDealerRepository dealerRepository,
            IEmployeeRepository employeeRepository)
        {
            _prospectRepository = prospectRepository;
            _prospectFollowUpRepository = prospectFollowUpRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _dealerRepository = dealerRepository;
            _employeeRepository = employeeRepository;
        }

        public List<Dealer> GetAllDealer()
        {
            return _dealerRepository.Find(d => d.isActive == 1).ToList<Dealer>();
        }

        public List<Employee> GetSalesByDealer(string dealerCode)
        {
            return _employeeRepository.Find(s => s.DealerCode == dealerCode && s.IsDeleted == false && s.IsSalesActive == 1).ToList<Employee>();
        }

        public List<DistributeProspectView> GetSelectedProspect(string prospectIds)
        {
            List<DistributeProspectView> distributeSuspectViews = new List<DistributeProspectView>();
            string[] ids = prospectIds.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                Prospect prospect = PopulateProspect(Convert.ToInt32(ids[i]));
                if (prospect != null)
                {
                    Lead lead = PopulateLead(prospect.CRMCustomerNum);
                    if (lead != null)
                    {
                        DistributeProspectView view = new DistributeProspectView();
                        view.ProspectId = prospect.ProspectID;
                        view.Title = lead.Gender == "1" ? "Mr" : "Mrs";
                        view.Name = lead.Name;
                        view.Phone = lead.CellNo;
                        view.LastPurchaseUnit = (lead.LeadsUnitTransactions != null && lead.LeadsUnitTransactions.Count() > 0) ?
                            lead.LeadsUnitTransactions.Select(unit => unit.UnitMarketName).LastOrDefault() : "";
                        view.LastDealer = prospect.LastDealerName;
                        view.LastSales = prospect.LastSalesName;
                        view.CurrentDealerCode = prospect.CurrentDealerCode;
                        view.CurrentDealer = _dealerRepository.Find(d => d.DealerCode == prospect.CurrentDealerCode).Single().DealerName;
                        view.CurrentSales = prospect.CurrentSalesName;
                        view.ExpireDate = prospect.ExpiredDate != null ? String.Format("{0:dd-MM-yyyy}", prospect.ExpiredDate) : String.Empty;

                        distributeSuspectViews.Add(view);
                    }
                }
            }

            return distributeSuspectViews;
        }

        private List<Prospect> PopulateProspects()
        {
            List<Prospect> populatedProspects = new List<Prospect>();
            var prospects = _prospectRepository.Find(p => p.IsActive == 1 && p.IsDeleted == false);
            foreach (Prospect prospect in prospects)
            {
                var prospectFollowUps = _prospectFollowUpRepository.Find(fu => fu.ProspectID == prospect.ProspectID);
                if (prospectFollowUps != null && prospectFollowUps.Count() > 0)
                {
                    prospect.ProspectFollowUps = new List<ProspectFollowUp>();
                    prospect.ProspectFollowUps.AddRange(prospectFollowUps);
                }
                populatedProspects.Add(prospect);
            }
            return populatedProspects;
        }

        private Prospect PopulateProspect(int prospectId)
        {
            Prospect prospect = _prospectRepository.Find(p => p.ProspectID == prospectId && p.IsActive == 1 && p.IsDeleted == false)
                .SingleOrDefault();
            if (prospect != null)
            {
                var prospectFollowUps = _prospectFollowUpRepository.Find(fu => fu.ProspectID == prospect.ProspectID);
                if (prospectFollowUps.Count() > 0)
                {
                    prospect.ProspectFollowUps = new List<ProspectFollowUp>();
                    prospect.ProspectFollowUps.AddRange(prospectFollowUps);
                }
            }
            return prospect;
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

        public bool DistributeProspect(List<DistributeProspectView> prospectViews, string dealer)
        {
            using(TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, 
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    foreach (DistributeProspectView view in prospectViews)
                    {
                        Prospect prospect = PopulateProspect(view.ProspectId);
                        prospect.CurrentDealerCode = view.CurrentDealerCode;
                        prospect.CurrentSalesName = view.CurrentSales;
                        prospect.ExpiredDate = DateTime.Now.AddDays(7);

                        _prospectRepository.Update(prospect);
                    }
                    ts.Complete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
