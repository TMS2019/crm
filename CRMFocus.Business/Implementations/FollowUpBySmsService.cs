using CRMFocus.Business.Interfaces;
using System.Collections.Generic;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System.Linq;
using CRMFocus.Common;

namespace CRMFocus.Business.Implementations
{
    public class FollowUpBySmsService : IFollowUpBySmsService
    {
        private readonly ISMSFollowupRepository _smsFollowupRepository;
        private readonly ILeadsRepository _leadRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly IMasterStatusRepository _masterStatusRepository;

        public FollowUpBySmsService(ISMSFollowupRepository smsFollowupRepository, ILeadsRepository leadRepository,
            IScenarioRepository scenarioRepository, ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            IMasterStatusRepository masterStatusRepository)
        {
            _smsFollowupRepository = smsFollowupRepository;
            _leadRepository = leadRepository;
            _scenarioRepository = scenarioRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _masterStatusRepository = masterStatusRepository;
        }

        public List<FollowUpBySmsView> GetAllFollowUpBySms()
        {
            var list = new List<FollowUpBySmsView>();
            var smsFollowUps = _smsFollowupRepository.GetAll().ToList();
            var scenarios = _scenarioRepository.GetAll().ToList();

            foreach (var item in scenarios)
            {
                var smsFollowUp = smsFollowUps.Where(w => w.ScenarioCode == item.ScenarioCode).FirstOrDefault();
                if (smsFollowUp != null)
                {
                    var model = new FollowUpBySmsView();

                    model.ScenarioCode = smsFollowUp.ScenarioCode;
                    model.NamaSkenario = smsFollowUp.Scenario.ScenarioName;
                    model.TipeSkenario = StaticType.GetScenarioType().Where(w => w.Key == smsFollowUp.Scenario.ResourceType).Select(s => s.Value).FirstOrDefault();
                    model.TanggalMulai = smsFollowUp.Scenario.StartDate;
                    model.TanggalSelesai = smsFollowUp.Scenario.EndDate;
                    model.Terkirim = smsFollowUps.Where(w => w.Status == 1 && w.ScenarioCode == item.ScenarioCode).Count(); // status == sms sent
                    model.Gagal = smsFollowUps.Where(w => w.Status == 2 && w.ScenarioCode == item.ScenarioCode).Count(); // status == sms not sent
                    model.Total = smsFollowUps.Where(w => w.ScenarioCode == item.ScenarioCode).Count();  // total sms based on scenario code

                    list.Add(model);
                }
            }

            return list;
        }

        public List<FollowUpBySmsDetailsView> GetAllDetailsFollowUpBySms(string scenarioCode)
        {
            var list = new List<FollowUpBySmsDetailsView>();
            var scenarioCodes = scenarioCode.Split(',').ToArray();
            var smsFollowUps = _smsFollowupRepository.GetAll().ToList();
            var leads = _leadRepository.GetAll().ToList();
            var scenarios = _scenarioRepository.GetAll().ToList();
            var leadsUnitTransactions = _leadsUnitTransactionRepository.GetAll().ToList();
            var smsStatuses = _masterStatusRepository.GetAll().Where(w => w.StatusGroup == "CRM_SMS_FL").ToList();

            for (int i = 0; i < scenarioCodes.Length; i++)
            {
                var smsFollowUpScenarios = smsFollowUps.Where(w => w.ScenarioCode == scenarioCodes[i]).ToList();
                var customerCodes = smsFollowUpScenarios.Select(s => s.CRMCustomerNum).ToList();
                var lead = leads.Where(w => customerCodes.Contains(w.CRMCustomerCode));

                foreach (var item in smsFollowUpScenarios)
                {
                    var model = new FollowUpBySmsDetailsView()
                    {
                        CRMCustomerNum = lead.Where(w => w.CRMCustomerCode == item.CRMCustomerNum).Select(s => s.CRMCustomerCode).FirstOrDefault(),
                        Nama = lead.Where(w => w.CRMCustomerCode == item.CRMCustomerNum).Select(s => s.Name).FirstOrDefault(),
                        Scenario = scenarios.Where(w => w.ScenarioCode == item.ScenarioCode).Select(s => s.ScenarioName).FirstOrDefault(),
                        Status = smsStatuses.Where(w => w.Value == item.Status).Select(s => s.Description).FirstOrDefault(),
                        Telepon = lead.Where(w => w.CRMCustomerCode == item.CRMCustomerNum).Select(s => s.CellNo).FirstOrDefault(),
                        Unit = leadsUnitTransactions.Where(w => w.CRMCustomerCode == item.CRMCustomerNum).Select(s => s.UnitMarketName).FirstOrDefault(),
                        TglTerkirim = item.Senddate,
                        TglUpload = item.CreatedTime
                    };


                    list.Add(model);
                }
            }

            return list;
        }
    }
}
