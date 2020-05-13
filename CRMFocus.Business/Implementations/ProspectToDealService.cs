using CRMFocus.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMFocus.Domain;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.Business.Implementations
{
    public class ProspectToDealService : IProspectToDealService
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IProspectFollowUpRepository _prospectFollowUpRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IMasterStatusRepository _masterStatusRepository;

        public ProspectToDealService(IProspectRepository prospectRepository,
            IProspectFollowUpRepository prospectFollowUpRepository,
            ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            IDealerRepository dealerRepository,
            IMasterStatusRepository masterStatusRepository)
        {
            _prospectRepository = prospectRepository;
            _prospectFollowUpRepository = prospectFollowUpRepository;
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _dealerRepository = dealerRepository;
            _masterStatusRepository = masterStatusRepository;
        }

        public List<ProspectView> GetAllProspect()
        {
            List<ProspectView> prospectViews = new List<ProspectView>();
            var prospects = PopulateProspects();

            foreach (Prospect prospect in prospects)
            {
                Lead lead = PopulateLead(prospect.CRMCustomerNum);
                if(lead != null)
                {
                    ProspectView prospectView = new ProspectView();
                    prospectView.ProspectId = prospect.ProspectID;
                    prospectView.Title = lead.Gender == "1" ? "Mr" : "Mrs";
                    prospectView.Name = lead.Name;
                    prospectView.Phone = lead.CellNo;
                    prospectView.LastPurchaseUnit = (lead.LeadsUnitTransactions != null && lead.LeadsUnitTransactions.Count() > 0)? 
                        lead.LeadsUnitTransactions.Select(unit => unit.UnitMarketName).LastOrDefault() : "";
                    prospectView.Dealer = prospect.LastDealerName;
                    prospectView.SalesName = prospect.LastSalesName;
                    prospectView.City = "";
                    prospectView.CurrentDealer = _dealerRepository.Find(d => d.DealerCode == prospect.CurrentDealerCode).Single().DealerName;
                    prospectView.FollowUp = (prospect.ProspectFollowUps != null && prospect.ProspectFollowUps.Count > 0) ? "Y" : "N";
                    prospectView.Status = (prospect.ProspectFollowUps != null && prospect.ProspectFollowUps.Count > 0) ? 
                        prospectStatus(prospect.ProspectFollowUps.OrderByDescending(fu => fu.FollowupDate).FirstOrDefault().ProspectStatus) : "Low";

                    prospectViews.Add(prospectView);
                }
            }
            return prospectViews;
        }

        private string prospectStatus(byte fuStatus)
        {
            var status = _masterStatusRepository.Find(ms => ms.StatusGroup == "CRM_DIRECT_FL" && ms.Value == fuStatus).Select(ms => ms.Description).FirstOrDefault();
            return status;
        }

        private List<Prospect> PopulateProspects()
        {
            List<Prospect> populatedProspects = new List<Prospect>();
            var prospects =_prospectRepository.Find(p => p.IsActive == 1 && p.IsDeleted == false);
            foreach (Prospect prospect in prospects)
            {
                var prospectFollowUps = _prospectFollowUpRepository.Find(fu => fu.ProspectID == prospect.ProspectID);
                if(prospectFollowUps != null && prospectFollowUps.Count() > 0)
                {
                    prospect.ProspectFollowUps = new List<ProspectFollowUp>();
                    prospect.ProspectFollowUps.AddRange(prospectFollowUps);
                }
                populatedProspects.Add(prospect);
            }
            return populatedProspects;
        }

        private List<Lead> PopulateLeads()
        {
            List<Lead> populatedLeads = new List<Lead>();
            var leads = _leadsRepository.GetAll();
            foreach (Lead lead in leads)
            {
                var leadsUnitTransactions = _leadsUnitTransactionRepository.Find(u=>u.CRMCustomerCode == lead.CRMCustomerCode);
                if(leadsUnitTransactions != null && leadsUnitTransactions.Count() > 0)
                {
                    lead.LeadsUnitTransactions = new List<LeadsUnitTransaction>();
                    lead.LeadsUnitTransactions.AddRange(leadsUnitTransactions);
                }
                populatedLeads.Add(lead);
            }
            return populatedLeads;
        }

        private Lead PopulateLead(int crmCustomerCode)
        {
            Lead lead = _leadsRepository.GetById(crmCustomerCode); // should this using repo.find?
            if(lead != null)
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
