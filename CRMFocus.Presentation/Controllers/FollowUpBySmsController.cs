using CRMFocus.Business.Interfaces;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class FollowUpBySmsController : Controller
    {
        private readonly IFollowUpBySmsService _followUpBySmsService;

        public FollowUpBySmsController(IFollowUpBySmsService followUpBySmsService)
        {
            _followUpBySmsService = followUpBySmsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _followUpBySmsService.GetAllFollowUpBySms();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string scenarioCode)
        {
            return View();
        }

        public ActionResult ReadDetails(string scenarioCode)
        {
            if (scenarioCode != null && scenarioCode != "" && scenarioCode != "0")
            {
                var data = _followUpBySmsService.GetAllDetailsFollowUpBySms(scenarioCode);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}