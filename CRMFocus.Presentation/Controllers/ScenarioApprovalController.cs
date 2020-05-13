using CRMFocus.Business.Interfaces;
using CRMFocus.Domain;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class ScenarioApprovalController : Controller
    {
        private readonly IScenarioService _scenarioService;
        private readonly IScenarioApprovalDetailService _scenarioApprovalDetailService;

        public ScenarioApprovalController(IScenarioService scenarioService, IScenarioApprovalDetailService scenarioApprovalDetailService)
        {
            _scenarioService = scenarioService;
            _scenarioApprovalDetailService = scenarioApprovalDetailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _scenarioApprovalDetailService.GetAllScenarios();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(string scenarioCode)
        {
            var model = _scenarioApprovalDetailService.DetailScenario(scenarioCode);

            if (model.HasHistories)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScenarioHistory(ScenarioApprovalDetailView scenarioApprovalDetailView, string decision)
        {
            try
            {
                scenarioApprovalDetailView.ScenarioHistoryView.IsApproved = decision == "Approve";
                _scenarioService.CreateScenarioHistory(scenarioApprovalDetailView);
                return RedirectToAction("Index");
            }
            catch
            {
                //TODO: perlu memunculkan error di frontend
                return RedirectToAction("Index");
            }
        }
    }
}