using CRMFocus.Domain;
using CRMFocus.Entity;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IScenarioApprovalDetailService
    {       
        ScenarioApprovalDetailView DetailScenario(string scenarioCode);
        List<ScenarioListView> GetAllScenarios();
    }
}
