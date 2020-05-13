using CRMFocus.Business.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Entity;
using CRMFocus.Presentation.Resolver;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace CRMFocus.Presentation.Controllers.Api
{
    public class ScenarioSettingController : ApiController
    {
        private readonly UserRoleResolver _userRole;
        private readonly IScenarioSettingService _scenarioSettingService;

        public ScenarioSettingController(IScenarioSettingService scenarioSettingService, UserRoleResolver userRole)
        {
            _userRole = userRole;
            _scenarioSettingService = scenarioSettingService;
        }

        [HttpGet]
        public List<ScenarioSettingView> GetAllScenarioSettingH1()
        {
            var role = _userRole.GetThisUserRole();
            return _scenarioSettingService.GetAllScenarioSetting(1);
        }

        [HttpGet]
        public List<ScenarioSettingView> GetAllScenarioSettingH2()
        {
            var role = _userRole.GetThisUserRole();
            return _scenarioSettingService.GetAllScenarioSetting(2);
        }

        [HttpGet]
        public List<ScenarioSettingView> GetAllScenarioSettingH3()
        {
            var role = _userRole.GetThisUserRole();
            return _scenarioSettingService.GetAllScenarioSetting(3);
        }

        [HttpGet]
        public List<ScenarioSettingView> GetAllCustomScenario()
        {
            var role = _userRole.GetThisUserRole();
            return _scenarioSettingService.GetAllGetAllCustomScenario();
        }

        [HttpPut]
        public void Update(ScenarioSettingView scenarioSetting)
        {
            var role = _userRole.GetThisUserRole();
            _scenarioSettingService.Update(scenarioSetting.ScenarioSettingViewId, scenarioSetting);
        }
    }
}
