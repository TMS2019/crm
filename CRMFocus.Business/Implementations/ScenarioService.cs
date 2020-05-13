using CRMFocus.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CRMFocus.Entity;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Common;
using System;
using System.Transactions;
using System.Data.Entity.Core;
using AutoMapper;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;


namespace CRMFocus.Business.Implementations
{
    public class ScenarioService : IScenarioService
    {
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IScenarioFilterRepository _scenarioFilterRepository;
        private readonly ICustomerProfileRefRepository _customerProfileRefRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IKabupatenRepository _kabupatenRepository;
        private readonly IKecamatanRepository _kecamatanRepository;
        private readonly IKelurahanRepository _kelurahanRepository;
        private readonly IScenarioHistoryRepository _scenarioHistoryRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IScenarioScriptMappingRepository _scenarioScriptMappingRepository;
        private readonly IUnityTypeMarketRepository _unityTypeMarketRepository;

        public ScenarioService(IScenarioRepository scenarioRepository, IScenarioFilterRepository scenarioFilterRepository,
            ICustomerProfileRefRepository customerProfileRefRepository, IProvinceRepository provinceRepository,
            IKabupatenRepository kabupatenRepository, IKecamatanRepository kecamatanRepository,
            IKelurahanRepository kelurahanRepository, IScenarioHistoryRepository scenarioHistoryRepository,
            IScriptRepository scriptRepository, IScenarioScriptMappingRepository scenarioScriptMappingRepository,
            IUnityTypeMarketRepository unityTypeMarketRepository)
        {
            _scenarioRepository = scenarioRepository;
            _scenarioFilterRepository = scenarioFilterRepository;
            _customerProfileRefRepository = customerProfileRefRepository;
            _provinceRepository = provinceRepository;
            _kabupatenRepository = kabupatenRepository;
            _kecamatanRepository = kecamatanRepository;
            _kelurahanRepository = kelurahanRepository;
            _scenarioHistoryRepository = scenarioHistoryRepository;
            _scriptRepository = scriptRepository;
            _scenarioScriptMappingRepository = scenarioScriptMappingRepository;
            _unityTypeMarketRepository = unityTypeMarketRepository;
        }

        public List<ScenarioListView> GetAllScenarios()
        {
            var scenarioViews = new List<ScenarioListView>();
            var scenarios = _scenarioRepository.GetAll().OrderBy(s => s.ScenarioHistory == null ? DateTime.Now : s.ScenarioHistory.Date).ToList();

            foreach (var item in scenarios)
            {
                var scenarioHistory = _scenarioHistoryRepository.Find(sh => sh.MappingHistoryCode == item.MappingHistoryCode).FirstOrDefault();

                var scenarioView = new ScenarioListView();
                scenarioView.ScenarioCode = item.ScenarioCode;
                scenarioView.NamaScenario = item.ScenarioName;
                scenarioView.Deskripsi = item.ScenarioDescription;
                scenarioView.TglSubmit = item.CreatedTime;
                scenarioView.Requester = item.SubmitionEmployeCode == null ? string.Empty : item.Employee.Name;
                scenarioView.Cabang = item.DealerCode == null ? string.Empty : item.Dealer.DealerName;
                scenarioView.TglMulai = item.StartDate;
                scenarioView.TglSelesai = item.EndDate;
                scenarioView.Status = item.StatusCode == null ? string.Empty : item.MasterStatus.Name;
                scenarioView.isActive = true;
                scenarioView.ScenarioHistoryRejectReason = scenarioHistory != null ? scenarioHistory.RejectReason : string.Empty;

                scenarioViews.Add(scenarioView);
            }

            return scenarioViews;
        }

        public List<CustomerProfileRef> GetAllCustomerProfileRef()
        {
            return _customerProfileRefRepository.GetAll().ToList();
        }

        public List<Province> GetAllProvince()
        {
            return _provinceRepository.GetAll().ToList();
        }

        public List<Kabupaten> GetAlKabupaten()
        {
            return _kabupatenRepository.GetAll().ToList();
        }

        public List<Kecamatan> GetAllKecamatan()
        {
            return _kecamatanRepository.GetAll().ToList();
        }

        public List<Kelurahan> GetAllKelurahan()
        {
            return _kelurahanRepository.GetAll().ToList();
        }

        public ScenarioApprovalDetailView CreateScenarioHistory(ScenarioApprovalDetailView scenarioApprovalDetailView)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var scenarioHistory = new ScenarioHistory();
                    scenarioHistory.RejectReason = scenarioApprovalDetailView.ScenarioHistoryView.RejectReason;
                    scenarioHistory.MappingHistoryCode = Guid.NewGuid().ToString().Substring(0, 5);
                    scenarioHistory.Date = DateTime.Now;

                    if (scenarioApprovalDetailView.ScenarioHistoryView.IsApproved)
                    {
                        // TODO : lanjutkan ketika oAuth udah jalan
                        //scenarioHistory.ApprovedByEmployee = 
                    }
                    else
                    {
                        // TODO : lanjutkan ketika oAuth udah jalan
                        //scenarioHistory.RejectedByEmployee
                    }

                    scenarioHistory = _scenarioHistoryRepository.Create(scenarioHistory);

                    var scenario = _scenarioRepository.GetAll().Where(w => w.ScenarioCode == scenarioApprovalDetailView.ScenarioCode).FirstOrDefault();
                    scenario.MappingHistoryCode = scenarioHistory.MappingHistoryCode;

                    _scenarioRepository.Update(scenario);
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            


            return scenarioApprovalDetailView;
        }

        public CreateScenarioView CreateScenario(CreateScenarioView createScenarioView)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var CRCode = GenerateScenarioCode(createScenarioView);

                    // Create Scenario
                    var scenario = new Scenario();
                    Mapper.Map(createScenarioView.TambahDetailScenarioView, scenario);
                    scenario.ScenarioCode = CRCode;
                    scenario = _scenarioRepository.Create(scenario);

                    // Create Targeting Customer
                    #region Create Targeting Customer
                    var scenarioFilter = new ScenarioFilter();
                    Mapper.Map(createScenarioView.TambahCustomerTargetingView, scenarioFilter);
                                                                                                                                                           // yyyy - mm - dd
                    string leadsOdataQuery = string.Format("CRMLeads?$filter=Profesion eq '{0}' and Gender eq '{1}' and Religion eq '{2}' and BirthDate le DATETIME'{3}-{4}-{5}' and BirthDate gt DATETIME'{6}-{7}-{8}' and Spending eq '{9}' and ",
                                            createScenarioView.TambahCustomerTargetingView.Profesion, createScenarioView.TambahCustomerTargetingView.Gender, createScenarioView.TambahCustomerTargetingView.Religion,
                                            createScenarioView.TambahCustomerTargetingView.Year1, createScenarioView.TambahCustomerTargetingView.BirthDate1.Date.Month, createScenarioView.TambahCustomerTargetingView.BirthDate1.Date.Day,
                                            createScenarioView.TambahCustomerTargetingView.Year2, createScenarioView.TambahCustomerTargetingView.BirthDate2.Date.Month, createScenarioView.TambahCustomerTargetingView.BirthDate2.Date.Day,
                                            createScenarioView.TambahCustomerTargetingView.Spending);

                    if (createScenarioView.TambahCustomerTargetingView.ResourceType == 1)
                    {
                        string leadsUnitTransactionOdataQuery = string.Format("CRMLeadsUnitTransaction/any(u: u/ProvinceCode eq '{0}' and u/KabupatenCode eq '{1}' and u/KecamatanCode eq '{2}' and u/KelurahanCode eq '{3}' and u/UnitTypeSegment eq '{4}' and u/UnitTypeSeries eq {5} and u/UnitMarketName eq '{6}' ",
                                           createScenarioView.TambahCustomerTargetingView.ProvinceCode, createScenarioView.TambahCustomerTargetingView.KabupatenCode, createScenarioView.TambahCustomerTargetingView.KecamatanCode, createScenarioView.TambahCustomerTargetingView.KelurahanCode,
                                           createScenarioView.TambahCustomerTargetingView.UnitTypeSegment, createScenarioView.TambahCustomerTargetingView.UnitTypeSeries, createScenarioView.TambahCustomerTargetingView.UnitMarketName);


                        if (createScenarioView.TambahCustomerTargetingView.FilterLainView.Count() != 0)
                        {
                            var count = createScenarioView.TambahCustomerTargetingView.FilterLainView.Count();
                            for (int i = 0; i < count; i++)
                            {
                                if (createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Section.ToLower() == "TglBeliDLR".ToLower() && createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Operator == "-")
                                {
                                    if (createScenarioView.TambahCustomerTargetingView.FilterLainView[i].DayOrMonth.ToLower() == "day")
                                    {
                                        var day = -(Convert.ToInt32(createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Jumlah));
                                        var startDateByDay = createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.AddDays(day);

                                        leadsUnitTransactionOdataQuery = leadsUnitTransactionOdataQuery + string.Format("{0} {1} eq le DATETIME'{2}-{3}-{4}' gt DATETIME'{5}-{6}-{7}' ",
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Logic,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Section,
                                                                            startDateByDay.Year, startDateByDay.Month, startDateByDay.Day,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Year,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Month,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Day);
                                    }
                                    else
                                    {
                                        var month = -(Convert.ToInt32(createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Jumlah));
                                        var startDateByMonth = createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.AddMonths(month);

                                        leadsUnitTransactionOdataQuery = leadsUnitTransactionOdataQuery + string.Format("{0} {1} eq le DATETIME'{2}-{3}-{4}' gt DATETIME'{5}-{6}-{7}' ",
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Logic,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Section,
                                                                            startDateByMonth.Year, startDateByMonth.Month, startDateByMonth.Day,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Year,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Month,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Day);
                                    }
                                }
                                else
                                {
                                    if (createScenarioView.TambahCustomerTargetingView.FilterLainView[i].DayOrMonth.ToLower() == "day")
                                    {
                                        var day = Convert.ToInt32(createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Jumlah);
                                        var startDateByDay = createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.AddDays(day);

                                        leadsUnitTransactionOdataQuery = leadsUnitTransactionOdataQuery + string.Format("{0} {1} eq le DATETIME'{2}-{3}-{4}' gt DATETIME'{5}-{6}-{7}' ",
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Logic,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Section,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Year,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Month,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Day,
                                                                            startDateByDay.Year, startDateByDay.Month, startDateByDay.Day);
                                    }
                                    else
                                    {
                                        var month = Convert.ToInt32(createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Jumlah);
                                        var startDateByMonth = createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.AddMonths(month);

                                        leadsUnitTransactionOdataQuery = leadsUnitTransactionOdataQuery + string.Format("{0} {1} eq le DATETIME'{2}-{3}-{4}' gt DATETIME'{5}-{6}-{7}' ",
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Logic,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].Section,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Year,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Month,
                                                                            createScenarioView.TambahCustomerTargetingView.FilterLainView[i].TanggalMulai.Value.Day,
                                                                            startDateByMonth.Year, startDateByMonth.Month, startDateByMonth.Day);
                                    }
                                }
                            }
                        }

                        string endingQuery = ")&$expand=CRMLeadsUnitTransaction&$inlinecount=allpages";
                        scenarioFilter.OdataQueryScript = leadsOdataQuery + leadsUnitTransactionOdataQuery + endingQuery;
                        var scenarioFilterCount = _scenarioFilterRepository.GetAll().Count();
                        scenarioFilter.FillerCode = (_scenarioFilterRepository.GetAll().Count() + 1).ToString();
                    }

                    scenarioFilter = _scenarioFilterRepository.Create(scenarioFilter);
                    #endregion

                    AddCallScripts(createScenarioView.TambahMediaView.CallScripts, scenario.ScenarioCode);

                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return createScenarioView;
        }

        public TambahCustomerTargetingView CreateScenarioFilter(TambahCustomerTargetingView scenarioFilterView)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var scenarioFilter = new ScenarioFilter();
                    Mapper.Map(scenarioFilterView, scenarioFilter);

                    scenarioFilter = _scenarioFilterRepository.Create(scenarioFilter);
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return scenarioFilterView;
        }


        private void AddCallScripts(List<CallScriptView> callScripts, string scenarioCode)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    Script previousQuestion = null;
                    int mappingSequence = 1;

                    foreach (CallScriptView question in callScripts)
                    {
                        //question first
                        Script s = new Script();
                        s.ScenarioCode = scenarioCode;
                        s.Text = question.Pertanyaan;
                        //ScriptType = 3 = Question
                        s.ScriptType = 3;
                        s.TypeQuestion = question.TipePertanyaan;

                        s = _scriptRepository.Create(s);

                        //Scenario Script Mapping
                        ScenarioScriptMapping ssm = new ScenarioScriptMapping
                        {
                            ScenarioScriptMappingCode = StaticType.GetUniqueKey(10),
                            ScriptCode = s.Scriptcode,
                            Squence = mappingSequence,
                            ScenarioCode = scenarioCode,
                            IsDeleted = false
                        };

                        ssm = _scenarioScriptMappingRepository.Create(ssm);

                        mappingSequence += 1;

                        //set nextQuestion of next loop
                        if (previousQuestion != null)
                        {
                            previousQuestion.NextQuestion = s.Scriptcode;
                            _scriptRepository.Update(previousQuestion);
                        }

                        previousQuestion = s;

                        int jSequence = 1;

                        foreach (var jawaban in question.Jawabans)
                        {
                            //ScriptType = 4 = Answer
                            Script j = new Script { ScenarioCode = scenarioCode, Text = jawaban.Text, RefCode = s.Scriptcode, ScriptType = 4 };
                            _scriptRepository.Create(j);

                            ScenarioScriptMapping ssmAnswer = new ScenarioScriptMapping
                            {
                                ScenarioScriptMappingCode = StaticType.GetUniqueKey(10),
                                ScriptCode = j.Scriptcode,
                                Squence = jSequence,
                                ScenarioCode = scenarioCode,
                                IsDeleted = false
                            };

                            jSequence += 1;

                            ssmAnswer = _scenarioScriptMappingRepository.Create(ssmAnswer);
                        }

                    }

                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Just for testing API straight, don't delete
        public void AddCallScripts2(CallScriptView[] callScripts, string scenarioCode)
        {
            List<CallScriptView> lst = callScripts.OfType<CallScriptView>().ToList(); //
            AddCallScripts(lst, scenarioCode);
        }



        private string GenerateScenarioCode(CreateScenarioView createScenarioView)
        {
            // generate Scenario Code
            var CRCode = string.Format("CR{0}", DateTime.Now.Year.ToString().Substring(2, 2));

            // resource type data (1 for H1, 2 for H2, 3 for H3 and 8 for ADP)
            if (createScenarioView.TambahCustomerTargetingView.ResourceType == 1)
            {
                CRCode = string.Format(CRCode + 1);
            }

            if (createScenarioView.TambahCustomerTargetingView.ResourceType == 2)
            {
                CRCode = string.Format(CRCode + 2);
            }

            if (createScenarioView.TambahCustomerTargetingView.ResourceType == 3)
            {
                CRCode = string.Format(CRCode + 3);
            }

            if (createScenarioView.TambahCustomerTargetingView.ResourceType == 8)
            {
                CRCode = string.Format(CRCode + 8);
            }

            // destionation type data (1 for H1, 2 untuk H2, 3 untuk H3)
            if (createScenarioView.TambahCustomerTargetingView.DestinationType == 1)
            {
                CRCode = string.Format(CRCode + 1);
            }

            if (createScenarioView.TambahCustomerTargetingView.DestinationType == 2)
            {
                CRCode = string.Format(CRCode + 2);
            }

            if (createScenarioView.TambahCustomerTargetingView.DestinationType == 3)
            {
                CRCode = string.Format(CRCode + 3);
            }

            var scenarioRepositoryCount = _scenarioRepository.GetAll().Count() + 1; // TO DO: get by dealer code
            if (scenarioRepositoryCount < 10)
            {
                CRCode = string.Format(CRCode + "00{0}", scenarioRepositoryCount);
            }
            if (scenarioRepositoryCount > 9 && scenarioRepositoryCount < 100)
            {
                CRCode = string.Format(CRCode + "0{0}", scenarioRepositoryCount);
            }
            if (scenarioRepositoryCount > 99 && scenarioRepositoryCount < 1000)
            {
                CRCode = string.Format(CRCode + "{0}", scenarioRepositoryCount);
            }

            return CRCode;
        }

        public int GetLeadDWAI(string baseAddress, Dictionary<string, string> credentials)
        {
            var leadsTotal = 0;

            try
            {
                using (var client = new HttpClient())
                {   
                    var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(credentials)).Result;
                    var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;

                  
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                    var resp = client.GetAsync(baseAddress + "/odata/CRMLeads");

                    resp.Wait(TimeSpan.FromSeconds(10));

                    if (resp.IsCompleted)
                    {
                        if (resp.Result.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            //Console.WriteLine("Authorization failed. Token expired or invalid.");
                        }
                        else
                        {
                            var response = resp.Result.Content.ReadAsStringAsync().Result;
                            var json = JObject.Parse(response);
                            
                            leadsTotal = json.Children().Children().Children().Count();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return leadsTotal;
        }

        public List<UnityTypeMarket> GetUnityTypeMarket()
        {
            return _unityTypeMarketRepository.GetAll().ToList();
        }
        
    }
}
