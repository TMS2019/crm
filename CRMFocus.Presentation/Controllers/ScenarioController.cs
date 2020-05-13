using CRMFocus.Business.Interfaces;
using CRMFocus.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CRMFocus.Common;
using System.Web.Script.Serialization;
using System.Web.Configuration;

namespace CRMFocus.Presentation.Controllers
{
    public class ScenarioController : Controller
    {
        private readonly IScenarioService _scenarioService;

        public ScenarioController(IScenarioService scenarioService)
        {
            _scenarioService = scenarioService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            var data = _scenarioService.GetAllScenarios();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKabupaten(string provinceCode)
        {
            var data = _scenarioService.GetAlKabupaten().Where(w => w.ProvinceCode == provinceCode).Select(s => new SelectListItem { Value = s.KabupatenCode, Text = s.KabupatenName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKecamatan(string kabupatenCode)
        {
            var data = _scenarioService.GetAllKecamatan().Where(w => w.KabupatenCode == kabupatenCode).Select(s => new SelectListItem { Value = s.KecamatanCode, Text = s.KecamatanName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKelurahan(string kecamatanCode)
        {
            var data = _scenarioService.GetAllKelurahan().Where(w => w.KecamatanCode == kecamatanCode).Select(s => new SelectListItem { Value = s.KelurahanCode, Text = s.KelurahanName });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            var customerProfileRefs = _scenarioService.GetAllCustomerProfileRef();
            ViewBag.Profesion = customerProfileRefs.Where(w => w.Type == "JOB").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Gender = customerProfileRefs.Where(w => w.Type == "GENDER").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Religion = customerProfileRefs.Where(w => w.Type == "RELIGION").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });
            ViewBag.Spending = customerProfileRefs.Where(w => w.Type == "MONTHEXPENSE").Select(s => new SelectListItem { Value = s.Value, Text = s.Text });

            ViewBag.Provinces = _scenarioService.GetAllProvince().Select(s => new SelectListItem { Value = s.ProvinceCode, Text = s.ProvinceName });
            ViewBag.Kabupatens = _scenarioService.GetAlKabupaten().Select(s => new SelectListItem { Value = s.KabupatenCode, Text = s.KabupatenName });
            ViewBag.Kecamatans = _scenarioService.GetAllKecamatan().Select(s => new SelectListItem { Value = s.KecamatanCode, Text = s.KecamatanName });
            ViewBag.Kelurahans = _scenarioService.GetAllKelurahan().Select(s => new SelectListItem { Value = s.KelurahanCode, Text = s.KelurahanName });

            ViewBag.ResourceType = StaticType.GetScenarioResourceTypes().Select(s => new SelectListItem { Value = s.Key.ToString(), Text = s.Value });
            ViewBag.DestinationType = StaticType.GetScenarioDestinationTypes().Select(s => new SelectListItem { Value = s.Key.ToString(), Text = s.Value });

            var marketSegment = _scenarioService.GetUnityTypeMarket().Select(s => s.UnitTypeSegment).Distinct().ToList();
            var marketSegmentSelectList = new List<SelectListItem>();
            for (int i = 0; i < marketSegment.Count(); i++)
            {
                var temp = new SelectListItem();
                temp.Text = marketSegment[i];
                temp.Value = marketSegment[i];
                marketSegmentSelectList.Add(temp);
            }
            ViewBag.MarketSegment = marketSegmentSelectList;

            ViewBag.MarketName = _scenarioService.GetUnityTypeMarket().GroupBy(x => new { x.UnitTypeCode, x.UnitMarketNameCode }).Select(s => new SelectListItem { Value = s.Key.UnitTypeCode, Text = s.Key.UnitMarketNameCode }); ;

            var marketType = _scenarioService.GetUnityTypeMarket().Select(s => s.UnitTypeSeries).Distinct().ToList();
            var marketTypeSelectList = new List<SelectListItem>();
            for (int i = 0; i < marketType.Count(); i++)
            {
                var temp = new SelectListItem();
                temp.Text = marketType[i];
                temp.Value = marketType[i];
                marketTypeSelectList.Add(temp);
            }
            ViewBag.MarketType = marketTypeSelectList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateScenarioView model)
        {
            try
            {
                _scenarioService.CreateScenario(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public int GetLeadDWAI()
        {
            var baseAddress = WebConfigurationManager.AppSettings["BaseAddress"];
            var credentials = new Dictionary<string, string>
            {
                {"grant_type", WebConfigurationManager.AppSettings["GrantTypeDWAI"]},
                {"username", WebConfigurationManager.AppSettings["UserNameDWAI"]},
                {"password", WebConfigurationManager.AppSettings["PasswordDWAI"]},
            };

            var data = _scenarioService.GetLeadDWAI(baseAddress, credentials);

            return data;
        }

        [HttpPost]
        //public ActionResult AddScenarioScript(string jsonObject)
        public ActionResult AddScenarioScript(string jsonObject)
        //{
        {
            //    //URL : /Scenario/AddScenarioScript (POST), param: jsonObject
            //This is just for testing ONLY, don't use this endpoint
            //    //jsonObject : [{"ScenarioCode":"ABCDE", "TipePertanyaan":1, "Pertanyaan": "How are you", "Jawabans":[{"Text":"Good"}, {"Text":"Bad"}, {"Text":"So-so"}]}]
            //URL : /Scenario/AddScenarioScript (POST), param: jsonObject
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //jsonObject : [{"TipePertanyaan":1, "Pertanyaan": "How are you", "Jawabans":[{"Text":"Good"}, {"Text":"Bad"}, {"Text":"So-so"}]}]
            //    CreateScenarioScriptView[] v = serializer.Deserialize<CreateScenarioScriptView[]>(jsonObject);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            CallScriptView[] v = serializer.Deserialize<CallScriptView[]>(jsonObject);


            //    _scenarioService.AddPhoneScript(v);
            _scenarioService.AddCallScripts2(v, "10000002");

            return Json("", JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult AddScenarioScript(string jsonObject)
        //{
        //    //URL : /Scenario/AddScenarioScript (POST), param: jsonObject
        //    //jsonObject : [{"ScenarioCode":"ABCDE", "TipePertanyaan":1, "Pertanyaan": "How are you", "Jawabans":[{"Text":"Good"}, {"Text":"Bad"}, {"Text":"So-so"}]}]
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    CreateScenarioScriptView[] v = serializer.Deserialize<CreateScenarioScriptView[]>(jsonObject);

        //    _scenarioService.AddPhoneScript(v);

        //    return Json("", JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public ActionResult CreateScenario(ScenarioView scenarioView)
        //{
        //    try
        //    {

        //        _scenarioService.CreateScenario(scenarioView);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult CreateScenarioFilter(ScenarioFilterView scenarioFilterView)
        //{
        //    try
        //    {

        //        _scenarioService.CreateScenarioFilter(scenarioFilterView);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}