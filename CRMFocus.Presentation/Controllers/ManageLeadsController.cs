using CRMFocus.Business.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class ManageLeadsController : Controller
    {
        private IManageLeadsService _manageLeadsService;

        public ManageLeadsController(IManageLeadsService manageLeadsService)
        {
            _manageLeadsService = manageLeadsService;
        }
        public ActionResult Index()
        {
            var dropdownList = new List<SelectListItem>();
            dropdownList.Add(new SelectListItem { Text = "Please Select", Value = "0" });

            var dealers = _manageLeadsService.GetDealerDropDown();
            foreach (var item in dealers)
            {
                dropdownList.Add(new SelectListItem { Text = item.DealerName, Value = item.DealerCode });
            }

            ViewBag.newDealer = dropdownList;

            return View();
        }

        public ActionResult Read()
        {
            var data = _manageLeadsService.GetAllLeads();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(string suspectIds, string currentDealer)
        {
            var data = _manageLeadsService.UpdateSuspectDealer(suspectIds, currentDealer);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}