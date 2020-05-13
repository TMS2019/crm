using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.Entity.Core;
using CRMFocus.Business.Interfaces;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Domain;
using CRMFocus.Entity;
using CRMFocus.Common;

namespace CRMFocus.Business.Implementations
{
    public class SuspectService : ISuspectService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly ILeadsUnitTransactionRepository _leadsUnitTransactionRepository;
        private readonly ISuspectRepository _suspectRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IMasterStatusRepository _masterStatusRepository;
        private readonly ISuspectFollowUpRepository _suspectFollowUpRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IScenarioScriptMappingRepository _scenarioScriptMappingRepository;
        private readonly IAnswerRepository _answerRepository;

        public SuspectService(ILeadsRepository leadsRepository,
            ILeadsUnitTransactionRepository leadsUnitTransactionRepository,
            ISuspectRepository suspectRepository,
            IScenarioRepository scenarioRepository,
            IMasterStatusRepository masterStatusRepository,
            ISuspectFollowUpRepository suspectFollowUpRepository,
            IDealerRepository dealerRepository,
            IScriptRepository scriptRepository,
            IScenarioScriptMappingRepository scenarioScriptMappingRepository,
            IAnswerRepository answerRepository)
        {
            _leadsRepository = leadsRepository;
            _leadsUnitTransactionRepository = leadsUnitTransactionRepository;
            _suspectRepository = suspectRepository;
            _scenarioRepository = scenarioRepository;
            _masterStatusRepository = masterStatusRepository;
            _suspectFollowUpRepository = suspectFollowUpRepository;
            _dealerRepository = dealerRepository;
            _scriptRepository = scriptRepository;
            _scenarioScriptMappingRepository = scenarioScriptMappingRepository;
            _answerRepository = answerRepository;
        }

        public List<SuspectView> GetAllSuspects()
        {
            var list = new List<SuspectView>();
            var suspects = _suspectRepository.Find(f => f.IsInactive == false && f.IsDeleted == false);
            var populateLeads = PopulateLeads();
            var scenarios = _scenarioRepository.GetAll();
            var units = _leadsUnitTransactionRepository.GetAll();
            var masterStatuses = _masterStatusRepository.GetAll();
            var dealers = _dealerRepository.GetAll().ToList();

            foreach (var item in suspects)
            {
                var lead = populateLeads.Find(f => f.CRMCustomerCode == item.CRMCustomerNum);
                if (lead != null)
                {
                    var suspectFollowUp = _suspectFollowUpRepository.Find(f => f.SuspectID == item.SuspectID).ToList();
                    var suspectView = new SuspectView();
                    var lastNote = suspectFollowUp.Select(s => s.Note).LastOrDefault();
                    suspectView.SuspectId = item.SuspectID;
                    suspectView.Title = lead.Gender == "1" ? "Mr" : "Mrs";
                    suspectView.Name = lead.Name;
                    suspectView.Phone = lead.CellNo;
                    suspectView.LastPurchaseUnit = units.Where(w => w.CRMCustomerCode == item.CRMCustomerNum).Select(s => s.UnitMarketName).LastOrDefault();
                    suspectView.Dealer = item.CurrentDealer == null ? string.Empty : dealers.Where(w => w.DealerCode == item.CurrentDealer).Select(s => s.DealerName).FirstOrDefault();//item.CurrentDealer == null ? item.LastDealerName : item.CurrentDealer;
                    suspectView.LastSalesName = item.LastSalesName;
                    suspectView.ScenarioName = scenarios.Where(w => w.ScenarioCode == item.ScenarioCode).Select(s => s.ScenarioName).FirstOrDefault();
                    // suspectView.CallLog = "<div class='box-log-red'>R</div><div class='box-log-red'>U</div><div class='box-log-white' data-toggle='modal' data-target='#myModal'></div>";
                    suspectView.CallLog = PopulateCallLog(item.SuspectID, item.ScenarioCode, suspectView.ScenarioName, item.LastReactivate != null, item.LastReactivate);
                    suspectView.Note = lastNote == null ? "-" : lastNote;
                    suspectView.CurrentSalesName = item.CurrentSalesName;

                    if (suspectFollowUp.Count() == 0)
                    {
                        suspectView.Status = "Low";
                    }
                    else
                    {
                        suspectView.Status = masterStatuses.Where(w => w.MasterStatusID == item.SuspectStatus).Select(s => s.Description).FirstOrDefault();
                    }
                    list.Add(suspectView);
                }
            }

            return list;
        }

        private string PopulateCallLog(string SuspectID, string ScenarioCode, string ScenarioName, bool isReactivate, DateTime? ReactivateDate)
        {
            var callLog = "";
            var suspectFollowUp = _suspectFollowUpRepository.Find(f => f.SuspectID == SuspectID && f.IsDeleted == false).ToList();

            if (isReactivate)
            {
                suspectFollowUp = _suspectFollowUpRepository.Find(f => f.SuspectID == SuspectID && f.FollowupDate > ReactivateDate && f.IsDeleted == false).ToList();
            }

            if (suspectFollowUp.Count() == 0)
            {
                callLog = "<div class='box-log-white callLogButton' data-toggle='modal' data-susid='" + SuspectID + "' data-scode='" + ScenarioCode + "' data-sname='" + ScenarioName + "' data-target='#myModal'></div><div class='box-log-white' data-toggle='modal' data-susid='" + SuspectID + "' data-scode='" + ScenarioCode + "' data-sname='" + ScenarioName + "' data-target='#myModal'></div><div class='box-log-white' data-toggle='modal' data-susid='" + SuspectID + "' data-scode='" + ScenarioCode + "' data-sname='" + ScenarioName + "' data-target='#myModal'></div>";
            }
            else
            {
                var masterStatuses = _masterStatusRepository.GetAll();
                foreach (var item in suspectFollowUp)
                {
                    var callStatus = masterStatuses.Where(w => w.MasterStatusID == item.CallStatus).Select(s => s.Description).FirstOrDefault();
                    callLog += "<div class='box-log-red'>" + callStatus[0] + "</div>";
                }

                var totalFollowUp = 3 - suspectFollowUp.Count();
                for (int i = 0; i < totalFollowUp; i++)
                {
                    callLog += "<div class='box-log-white callLogButton' data-toggle='modal' data-susid='" + SuspectID + "' data-scode='" + ScenarioCode + "' data-sname='" + ScenarioName + "' data-target='#myModal'></div>";
                }
            }

            return callLog;
        }

        private List<Lead> PopulateLeads()
        {
            var list = new List<Lead>();
            var leads = _leadsRepository.GetAll();
            var leadsUnitTransactions = _leadsUnitTransactionRepository.GetAll();

            foreach (var item in leads)
            {
                var result = leadsUnitTransactions.Where(w => w.CRMCustomerCode == item.CRMCustomerCode);
                if (result.Count() > 0)
                {
                    item.LeadsUnitTransactions = new List<LeadsUnitTransaction>();
                    item.LeadsUnitTransactions.AddRange(result);
                }
                list.Add(item);
            }

            return list;
        }

        public string DeactivateSuspect(string[] suspectIds)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    for (int i = 0; i < suspectIds.Length; i++)
                    {
                        var suspectId = suspectIds[i].ToString();
                        var suspect = _suspectRepository.Find(f => f.SuspectID == suspectId).FirstOrDefault();
                        if (suspect != null)
                        {
                            suspect.IsInactive = true;
                            _suspectRepository.Update(suspect);
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

            return "";

        }

        public void DeleteSuspect(string[] suspectIds)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    for (int i = 0; i < suspectIds.Length; i++)
                    {
                        var suspectId = suspectIds[i].ToString();
                        var suspect = _suspectRepository.Find(f => f.SuspectID == suspectId).FirstOrDefault();
                        if (suspect != null)
                        {
                            suspect.IsDeleted = true;
                            _suspectRepository.Update(suspect);
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

        public string UpdateStatus(string suspectID, int callStatus, string nextFollowup, string note)
        {
            /*
             *  -- Call Status
             *  Id Value Description
                3	0	 Open
                4	1	 Workload
                5	2	 Contacted
                6	3	 Rejected
                7	4	 Unreachable

                -- Suspect Status
                Id Value Description
                8	1	 Hot
                9	2	 Medium
                10	3	 Low
                11	4	 Tidak Tertarik
             */

            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var suspect = _suspectRepository.Find(f => f.SuspectID == suspectID).FirstOrDefault();
                    var nextFollowupDate = nextFollowup == "" ? DateTime.Now : Convert.ToDateTime(nextFollowup);

                    if (nextFollowup == "" && callStatus == 5) // 5 == Call Status is Contacted
                    {
                        suspect.IsDeleted = true;
                        suspect.SuspectStatus = 10; // 10 == Suspect Status is Low
                        _suspectRepository.Update(suspect);
                    }
                    else if (nextFollowup != "" && callStatus == 5) // 5 == Call Status is Contacted
                    {
                        suspect.IsDeleted = true;

                        TimeSpan diff = nextFollowupDate - DateTime.Now;
                        var dateDiff = diff.Days;

                        if (dateDiff < 7)
                        {
                            suspect.SuspectStatus = 8; // 8 == Suspect Status is Hot
                            _suspectRepository.Update(suspect);
                        }
                        else if (dateDiff < 31)
                        {
                            suspect.SuspectStatus = 9; // 9 == Suspect Status is Medium
                            _suspectRepository.Update(suspect);
                        }
                        else
                        {
                            suspect.SuspectStatus = 10; // 10 == Suspect Status is Low
                            _suspectRepository.Update(suspect);
                        }


                    }
                    else if (callStatus == 6) // 5 == Call Status is Rejected
                    {
                        suspect.SuspectStatus = 10; // 10 == Suspect Status is Low
                        _suspectRepository.Update(suspect);
                    }
                    else if (callStatus == 7) // 7 == Call Status is Unreachable
                    {
                        suspect.SuspectStatus = 10; // 10 == Suspect Status is Low
                        _suspectRepository.Update(suspect);
                    }
                    //else
                    //{
                    //    TimeSpan diff = nextFollowupDate - DateTime.Now;
                    //    var dateDiff = diff.Days;

                    //    if (dateDiff < 7)
                    //    {
                    //        suspect.SuspectStatus = 8; // 8 == Suspect Status is Hot
                    //        _suspectRepository.Update(suspect);
                    //    }
                    //    else if (dateDiff < 31)
                    //    {
                    //        suspect.SuspectStatus = 9; // 9 == Suspect Status is Medium
                    //        _suspectRepository.Update(suspect);
                    //    }
                    //    else
                    //    {
                    //        suspect.SuspectStatus = 10; // 10 == Suspect Status is Low
                    //        _suspectRepository.Update(suspect);
                    //    }
                    //}

                    DateTime dateTimeNow = DateTime.Now;
                    var model = new SuspectFollowUp()
                    {
                        Id = 1,
                        SuspectFollowupID = Guid.NewGuid().ToString().Substring(0, 5),
                        SuspectID = suspectID,
                        CallStatus = callStatus,
                        FollowupDate = DateTime.Now,
                        Note = note,
                        NextFollowupDate = nextFollowupDate
                    };

                    _suspectFollowUpRepository.Create(model);


                    var suspectFollowUp = _suspectFollowUpRepository.Find(f => f.SuspectID == suspectID).ToList();
                    var setInactive = 0;
                    if (suspectFollowUp.Count() > 0)
                    {
                        foreach (var item in suspectFollowUp)
                        {
                            if (item.CallStatus == 6) // // 6 == Call Status is Unreachable
                            {
                                setInactive++;
                            }

                            if (item.CallStatus == 7) // 7 == Call Status is Unreachable
                            {
                                setInactive++;
                            }
                        }
                    }

                    if (setInactive > 2)
                    {
                        suspect.IsInactive = true;
                        _suspectRepository.Update(suspect);
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

            return "";

        }

        public void SubmitAnswer(AnswerOfScriptView asv)    
        {
            foreach (var qa in asv.Qas)
            {
                //find scenario code
                //we know the script code for the question
                var questionScriptCode = qa.ScriptCode;
                var ssm = _scenarioScriptMappingRepository.Find(s => s.ScriptCode == questionScriptCode).FirstOrDefault();

                if (ssm != null)
                {
                    foreach (var answer in qa.Jawabans)
                    {
                        var answerSsm = _scenarioScriptMappingRepository.Find(s => s.ScriptCode == answer.Value).FirstOrDefault();
                        Answer a = new Answer
                        {
                            CustomerCode = asv.CustomerCode,
                            ScenarioCode = ssm.ScenarioCode,
                            ScenarioAnswerCode = StaticType.GetUniqueKey(10),
                            IDAsk = ssm.Script.Scriptcode,
                            IDAnswer = answer.Value != 0 ? answer.Value : 0,
                            Handling = answer.Text,
                            ScenerioScriptMappingCode = answerSsm == null ? null : answerSsm.ScenarioScriptMappingCode
                        };

                        _answerRepository.Create(a);
                    }
                }
            }
        }

        public List<CallScriptView> GetCallScripts(string scenarioCode)
        {
            //get all questions ScriptType == 3
            var callScriptViews = new List<CallScriptView>();
            var questions = _scriptRepository.Find(sc => sc.ScenarioCode == scenarioCode && sc.ScriptType == 3 && sc.IsDeleted == false).ToList();
            var questions_without_answers = _scriptRepository.Find(sc => sc.ScenarioCode == scenarioCode && sc.RefCode == 0 && sc.TypeQuestion == 0 && sc.IsDeleted == false).ToList();
            if (questions != null)
            {

                foreach (var q in questions)
                {
                    CallScriptView csv = new CallScriptView { TipePertanyaan = q.TypeQuestion, Pertanyaan = q.Text, ScriptCode = q.Scriptcode };

                    //try to get answers
                    var answers = _scriptRepository.Find(sc => sc.ScenarioCode == scenarioCode && sc.ScriptType == 4 && sc.RefCode == q.Id).ToList();
                    if (answers != null)
                    {
                        var answerViews = new List<ScriptAnswerView>();
                        foreach (var answer in answers)
                        {
                            answerViews.Add(new ScriptAnswerView { Text = answer.Text, Value = answer.Scriptcode });
                        }

                        csv.Jawabans = answerViews;
                    }

                    callScriptViews.Add(csv);
                }

            }
            if (questions_without_answers != null)
            {
                foreach (var q in questions_without_answers)
                {
                    CallScriptView csv = new CallScriptView { TipePertanyaan = q.ScriptType, Pertanyaan = q.Text, ScriptCode = q.Scriptcode };
                    callScriptViews.Add(csv);
                }
            }
            return callScriptViews;
        }

        public List<SuspectCustomerView> GetDataCustomer(string suspectID)
        {
            var CustomerViews = new List<SuspectCustomerView>();
            var populateLeads = PopulateLeads();
            var detail = _suspectRepository.Find(f => f.SuspectID == suspectID).FirstOrDefault();
            var lead = populateLeads.Find(f => f.CRMCustomerCode == detail.CRMCustomerNum);
            var suspectFollowUp = _suspectFollowUpRepository.Find(f => f.SuspectID == detail.SuspectID).ToList();
            var dealers = _dealerRepository.GetAll();
            if (lead != null)
            {
                var SuspectCustomerView = new SuspectCustomerView();
                var lastNote = suspectFollowUp.Select(s => s.Note).LastOrDefault();
                SuspectCustomerView.SuspectId = detail.SuspectID;
                SuspectCustomerView.Name = lead.Name;
                SuspectCustomerView.Telepon = lead.CellNo;
                SuspectCustomerView.Email = lead.Email;
                SuspectCustomerView.PembelianTerakhir = detail.LastPurchaseUnit;
                SuspectCustomerView.CabangBaru = dealers.Where(w => w.DealerCode == detail.CurrentDealer).Select(s => s.DealerName).FirstOrDefault();
                SuspectCustomerView.LastSalesName = detail.LastSalesName;
                SuspectCustomerView.CurrentSalesName = detail.CurrentSalesName;
                SuspectCustomerView.Note = lastNote == null ? "-" : lastNote;
                CustomerViews.Add(SuspectCustomerView);
            }
            return CustomerViews;
        }

    }
}
