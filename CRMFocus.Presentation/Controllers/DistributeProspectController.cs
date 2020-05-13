using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class DistributeProspectController : Controller
    {
        private readonly IDistributeProspectService _distributeProspectService;
        public DistributeProspectController(IDistributeProspectService distributeProspectService)
        {
            _distributeProspectService = distributeProspectService;
        }
        // GET: DistributeProspect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string prospectIds)
        {
            ViewBag.Dealers = _distributeProspectService.GetAllDealer()
                .Select(d => new SelectListItem { Value = d.DealerCode, Text = d.DealerName });
            ViewBag.Sales = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "NO DATA FOUND" } };
            ViewBag.ProspectIds = prospectIds;

            return View();
        }

        public ActionResult GetSelectedProspect(string prospectIds)
        {
            var data = _distributeProspectService.GetSelectedProspect(prospectIds);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadSales(string dealerCode)
        {
            var data = _distributeProspectService.GetSalesByDealer(dealerCode).
                Select(s => new SelectListItem { Value = s.CRMID.ToString(), Text = s.Name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(string distributeProspectView)
        {
            //TODO : do real logic thing 
            return Json("sukses", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Distribute(List<DistributeProspectView> distributeProspectView, string dealer)
        {
            String message = "";
            if (_distributeProspectService.DistributeProspect(distributeProspectView, dealer))
                message = "Successfully distributed";
            else
                message = "Fail";
           return Json(message, JsonRequestBehavior.AllowGet);
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
