using CRMFocus.Common;
using CRMFocus.Domain;
using CRMFocus.Entity;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IScenarioSettingService : IBaseService<ScenarioSetting>
    {
        List<ScenarioSettingView> GetAllScenarioSetting(int destination);
        List<ScenarioSettingView> GetAllGetAllCustomScenario();
        ScenarioSettingView Update(string id, ScenarioSettingView newEntity);
    }
}
