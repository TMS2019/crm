using CRMFocus.Domain;
using CRMFocus.Entity;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IScenarioService
    {
        List<ScenarioListView> GetAllScenarios();
        CreateScenarioView CreateScenario(CreateScenarioView createScenarioView);
        int GetLeadDWAI(string baseAddress, Dictionary<string, string> credentials);
        ScenarioApprovalDetailView CreateScenarioHistory(ScenarioApprovalDetailView scenarioApprovalDetailView);
        TambahCustomerTargetingView CreateScenarioFilter(TambahCustomerTargetingView scenarioFilterView);
        List<CustomerProfileRef> GetAllCustomerProfileRef();
        List<Province> GetAllProvince();
        List<Kabupaten> GetAlKabupaten();
        List<Kecamatan> GetAllKecamatan();
        List<Kelurahan> GetAllKelurahan();
        List<UnityTypeMarket> GetUnityTypeMarket();
        void AddCallScripts2(CallScriptView[] callScripts, string scenarioCode);
    }
}
