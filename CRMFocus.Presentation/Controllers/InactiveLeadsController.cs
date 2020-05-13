using CRMFocus.Business.Interfaces;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class InactiveLeadsController : Controller
    {
        private readonly IInactiveLeadsService _inactiveLeadsService;

        public InactiveLeadsController(IInactiveLeadsService inactiveLeadsService)
        {
            _inactiveLeadsService = inactiveLeadsService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _inactiveLeadsService.GetAllInactiveLeads();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReactivateSuspect(string suspectIds)
        {
            var data = _inactiveLeadsService.ReactivateSuspect(suspectIds);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSuspect(string suspectIds)
        {
            var data = _inactiveLeadsService.DeleteSuspect(suspectIds);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}