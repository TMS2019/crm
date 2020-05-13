using CRMFocus.Business.Interfaces;
using CRMFocus.Common;
using CRMFocus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMFocus.Presentation.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ILeadsService _leadsService;
        public LeadsController(ILeadsService leadsService)
        {
            _leadsService = leadsService;
        }

        // GET: Leads
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _leadsService.GetAllLeadsTemporary();

            return Json(data, JsonRequestBehavior.AllowGet);
        } 

        public ActionResult ReadAllCustomerProfileRef()
        {
            var data = _leadsService.GetAllCustomerProfileRef();
         
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllProvince()
        {
            var data = _leadsService.GetAllProvince(); 
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllKabupaten()
        {
            var data = _leadsService.GetAlKabupaten();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllKecamatan()
        {
            var data = _leadsService.GetAllKecamatan();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAllKelurahan()
        {
            var data = _leadsService.GetAllKelurahan();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> ReadMarketType()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Testing Type" }
            };

            return data;

        }

        public List<SelectListItem> ReadMarketSegment()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Testing Segment" }
            };

            return data;

        }

        public List<SelectListItem> ReadMarketName()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Testing Market Name" }
            };

            return data;

        }


        public List<SelectListItem> ReadPaymentType()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Testing Payment type" }
            };

            return data;

        }

        public List<SelectListItem> ReadServiceType()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Testing Service type" }
            };

            return data;

        }

        public List<SelectListItem> ReadTitle()
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Mr" },
                new SelectListItem() { Value = "2", Text = "Mrs" },
            };

            return data;

        }

        // GET: Leads/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Leads/Create
        public ActionResult Create()
        {
            ViewBag.Provinces = _leadsService.GetAllProvince().Select(s => new SelectListItem { Value = s.ProvinceCode, Text = s.ProvinceName });
            ViewBag.Profesion = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "JOB").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Education = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "EDUCATION").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            //var customerProfileRefs = _leadsService.GetAllCustomerProfileRef();
            ViewBag.Gender = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "GENDER").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Religion = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "RELIGION").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Spending = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "MONTHEXPENSE").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });

            ViewBag.Provinsi = _leadsService.GetAllProvince().Select(s => new SelectListItem { Value = s.ProvinceCode, Text = s.ProvinceName });
            ViewBag.Kabupaten = _leadsService.GetAlKabupaten().Select(s => new SelectListItem { Value = s.KabupatenCode, Text = s.KabupatenName });
            ViewBag.Kecamatan = _leadsService.GetAllKecamatan().Select(s => new SelectListItem { Value = s.KecamatanCode, Text = s.KecamatanName });
            ViewBag.Kelurahan = _leadsService.GetAllKelurahan().Select(s => new SelectListItem { Value = s.KelurahanCode, Text = s.KelurahanName });
            ViewBag.CustomerCode = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "CUSTSALESTYPE").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });


            var marketSegment = _leadsService.GetUnityTypeMarket().Select(s => s.UnitTypeSegment).Distinct().ToList();
            var marketSegmentSelectList = new List<SelectListItem>();
            for (int i = 0; i < marketSegment.Count(); i++)
            {
                var temp = new SelectListItem();
                temp.Text = marketSegment[i];
                temp.Value = marketSegment[i];
                marketSegmentSelectList.Add(temp);
            }
            ViewBag.UnitTypeSegment = marketSegmentSelectList;

            ViewBag.UnitMarketName = _leadsService.GetUnityTypeMarket().GroupBy(x => new { x.UnitTypeCode, x.UnitMarketNameCode }).Select(s => new SelectListItem { Value = s.Key.UnitTypeCode, Text = s.Key.UnitMarketNameCode }); ;

            var marketType = _leadsService.GetUnityTypeMarket().Select(s => s.UnitTypeSeries).Distinct().ToList();
            var marketTypeSelectList = new List<SelectListItem>();
            for (int i = 0; i < marketType.Count(); i++)
            {
                var temp = new SelectListItem();
                temp.Text = marketType[i];
                temp.Value = marketType[i];
                marketTypeSelectList.Add(temp);
            }
            ViewBag.UnitTypeSeries = marketTypeSelectList;
            ViewBag.PaymentType = _leadsService.GetAllCustomerProfileRef().Where(w => w.Type == "PAYTYPE").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.ServiceType = ReadServiceType();
            ViewBag.GetTitle = ReadTitle();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateLeadsView model)
         {
            try
            {
                if (_leadsService.Create(model)) { 
                        return Json(new BaseMessage { Message = "Data saved successfully", Status = Utilities.SucceedStatus }, JsonRequestBehavior.AllowGet);
                 }

                 return Json(new BaseMessage { Message = "Failed to save data", Status = Utilities.SucceedStatus }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        // GET: Leads/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Leads/Edit/5
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

        // GET: Leads/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Leads/Delete/5
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

        public ActionResult ReadKabupaten(string provinceCode)
        {
            var data = _leadsService.GetAlKabupaten().Where(w => w.ProvinceCode == provinceCode).Select(s => new SelectListItem { Value = s.KabupatenCode, Text = s.KabupatenName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKecamatan(string kabupatenCode)
        {
            var data = _leadsService.GetAllKecamatan().Where(w => w.KabupatenCode == kabupatenCode).Select(s => new SelectListItem { Value = s.KecamatanCode, Text = s.KecamatanName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKelurahan(string kecamatanCode)
        {
            var data = _leadsService.GetAllKelurahan().Where(w => w.KecamatanCode == kecamatanCode).Select(s => new SelectListItem { Value = s.KelurahanCode, Text = s.KelurahanName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
