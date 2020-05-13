using CRMFocus.Business.Interfaces;
using CRMFocus.Common;
using CRMFocus.Presentation.Resolver;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class ManageSendSmsController : Controller
    {
        private readonly UserRoleResolver _userRole;
        private readonly IManageSendSmsService _manageSendSmsService;

        public ManageSendSmsController(IManageSendSmsService manageSendSmsService, UserRoleResolver userRole)
        {
            _userRole = userRole;
            _manageSendSmsService = manageSendSmsService;
        }

        public ActionResult Index()
        {
            var scenarioTypeDDL = new List<SelectListItem>();
            scenarioTypeDDL.Add(new SelectListItem { Text = "Please Select", Value = "0" });

            var scenarioTypes = StaticType.GetScenarioType();
            foreach (var item in scenarioTypes)
            {
                scenarioTypeDDL.Add(new SelectListItem { Text = item.Value, Value = item.Key.ToString() });
            }
            ViewBag.scenarioType = scenarioTypeDDL;


            var scenarioNameDDL = new List<SelectListItem>();
            scenarioNameDDL.Add(new SelectListItem { Text = "Please Select", Value = "0" });

            var scenarios = _manageSendSmsService.GetScenarioDropDown();
            foreach (var item in scenarios)
            {
                scenarioNameDDL.Add(new SelectListItem { Text = item.ScenarioName, Value = item.ScenarioCode });
            }           
            ViewBag.scenarioName = scenarioNameDDL;

            ViewBag.UserRole = _userRole.GetThisUserRole();

            return View();
        }

        [HttpPost]
        public ActionResult PreviewExcell()
        {
            HttpFileCollectionBase files = Request.Files;
            var result = _manageSendSmsService.GetPreviewExcell(files, _userRole.GetThisUserRole());

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(string[] scenarioLeadMappingViewIds, string scenarioCode, string scenarioType)
        {
            if (scenarioLeadMappingViewIds[0] != "")
            {
                var model = _manageSendSmsService.SaveExcell(scenarioLeadMappingViewIds, _userRole.GetThisUserRole(), scenarioCode, scenarioType);
                return Json(new BaseMessage() { Message = model.Message, Status = model.Status},
                    JsonRequestBehavior.AllowGet);
            }

            return Json(new BaseMessage() { Message = Utilities.SaveInComplete, Status = Utilities.ErrorStatus },
                JsonRequestBehavior.AllowGet);
        }
    }
}