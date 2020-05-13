using CRMFocus.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class ProspectToDealController : Controller
    {
        private readonly IProspectToDealService _prospectToDealService;

        public ProspectToDealController(IProspectToDealService prospectToDealService)
        {
            _prospectToDealService = prospectToDealService;
        }

        // GET: ProspectToDeal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _prospectToDealService.GetAllProspect();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: ProspectToDeal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProspectToDeal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProspectToDeal/Create
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

        // GET: ProspectToDeal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProspectToDeal/Edit/5
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

        // GET: ProspectToDeal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProspectToDeal/Delete/5
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
