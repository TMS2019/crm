using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CRMFocus.Presentation.Controllers
{
    public class DistributeSuspectController : Controller
    {
        private readonly IDistributeSuspectService _distributeSuspectService;
        public DistributeSuspectController(IDistributeSuspectService distributeSuspectService)
        {
            _distributeSuspectService = distributeSuspectService;
        }
        // GET: DistributeProspect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string suspectIds)
        {
            ViewBag.Dealers = _distributeSuspectService.GetAllDealer()
                .Select(d => new SelectListItem { Value = d.DealerCode, Text = d.DealerName });
            ViewBag.Sales = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "NO DATA FOUND" } };
            ViewBag.SuspectIds = suspectIds;

            return View();
        }

        public ActionResult GetSelectedSuspect(string suspectIds)
        {
            var data = _distributeSuspectService.GetSelectedSuspect(suspectIds);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadSales(string dealerCode)
        {
            var data = _distributeSuspectService.GetSalesByDealer(dealerCode).
                Select(s => new SelectListItem { Value = s.CRMID.ToString(), Text = s.Name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(string updatedSuspects)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DistributeSuspectView[] v = serializer.Deserialize<DistributeSuspectView[]>(updatedSuspects);
            
            var data = _distributeSuspectService.UpdateSuspects(v);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: DistributeProspect/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DistributeProspect/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistributeProspect/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DistributeProspect/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DistributeProspect/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DistributeProspect/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DistributeProspect/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
