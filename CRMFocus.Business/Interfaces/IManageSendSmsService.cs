using CRMFocus.Domain;
using CRMFocus.Entity;
using System.Collections.Generic;
using System.Web;

namespace CRMFocus.Business.Interfaces
{
    public interface IManageSendSmsService
    {
        List<Scenario> GetScenarioDropDown();
        List<ScenarioLeadMappingView> GetPreviewExcell(HttpFileCollectionBase files, string userRole);
        ScenarioLeadMappingView SaveExcell(string[] scenarioLeadMappingViewIds, string userRole, string scenarioCode, string scenarioType);
    }
}
