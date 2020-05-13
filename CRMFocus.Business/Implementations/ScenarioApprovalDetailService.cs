using CRMFocus.Business.Interfaces;
using System.Linq;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System;
using System.Collections.Generic;

namespace CRMFocus.Business.Implementations
{
    public class ScenarioApprovalDetailService : IScenarioApprovalDetailService
    {
        private readonly IScenarioRepository _scenarioRepository;

        public ScenarioApprovalDetailService(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;

        }

        public List<ScenarioListView> GetAllScenarios()
        {
            var scenarioViews = new List<ScenarioListView>();
            var scenarios =_scenarioRepository.Find(s => s.MappingHistoryCode == null && s.isDefault == 0).ToList();
            foreach (var item in scenarios)
            {
                var scenarioView = new ScenarioListView();
                scenarioView.ScenarioCode = item.ScenarioCode;
                scenarioView.NamaScenario = item.ScenarioName;
                scenarioView.Deskripsi = item.ScenarioDescription;
                scenarioView.TglSubmit = item.CreatedTime;
                scenarioView.Requester = item.SubmitionEmployeCode == null ? string.Empty : item.Employee.Name;
                scenarioView.Cabang = item.DealerCode == null ? string.Empty : item.Dealer.DealerName;
                scenarioView.TglMulai = item.StartDate;
                scenarioView.TglSelesai = item.EndDate;
                scenarioView.Status = item.StatusCode == null ? string.Empty : item.MasterStatus.Name;
                scenarioView.isActive = true;

                scenarioViews.Add(scenarioView);
            }

            return scenarioViews;
        }

        public ScenarioApprovalDetailView DetailScenario(string scenarioCode)
        {
            var detail = _scenarioRepository.Find(f => f.ScenarioCode == scenarioCode).FirstOrDefault();

            var scenarioView = new ScenarioApprovalDetailView();
            scenarioView.TargetCustomerName = detail.ScenarioFilter == null ? string.Empty : detail.ScenarioFilter.TargetCustumerName;
            scenarioView.ODataQueryScript = detail.ScenarioFilter == null ? string.Empty : detail.ScenarioFilter.OdataQueryScript;
            scenarioView.HasHistories = detail.ScenarioHistory != null;
            scenarioView.ScenarioCode = scenarioCode;

            return scenarioView;
            
        }
    }
}
