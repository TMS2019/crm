using CRMFocus.DataAccess;
using CRMFocus.Entity;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CRMFocus.Business.Implementations
{
    public class ScenarioDistributionJobService
    {
        private static readonly HttpClient client = new HttpClient();
        static readonly log4net.ILog logger = LogManager.GetLogger("logSendSmsScenario");

        public static void SendSMSAsync(string SMSGatewayAPI)
        {
            using (var db = new CRMFocusContext())
            {
                var scenarios = db.Scenarios.Where(w => DateTime.Now > w.StartDistributionSMSDate && DateTime.Now < w.EndDistributionSMSDate && w.isSMS == 1).ToList();
                foreach (var scenario in scenarios)
                {
                    var smsFollowups = db.SMSFollowups.Where(w => w.ScenarioCode == scenario.ScenarioCode).Select(s => s.CRMCustomerNum);
                    var leadMapping = db.ScenarioLeadMappings.Where(w => w.ScenarioCode == scenario.ScenarioCode && !smsFollowups.Contains(w.CRMCustomerNum)).ToList();
                    var scenarioSetting = db.ScenarioSettings.Where(w => w.ScenarioCode == scenario.ScenarioCode).FirstOrDefault();
                    var script = db.Scripts.Where(w => w.ScenarioCode == scenario.ScenarioCode);
                    var smsText = script.FirstOrDefault().Text;

                    var maxSms = 0;
                    if (scenarioSetting != null)
                    {
                        maxSms = scenarioSetting.MaxSms;
                    }

                    if (leadMapping != null)
                    {
                        var i = 0;
                        foreach (var leads in leadMapping)
                        {
                            if (scenarioSetting != null && i == maxSms)
                            {
                                break;
                            }

                            var customerCode = leads.CRMCustomerNum;
                            var lead = db.Leads.Where(w => w.CRMCustomerCode == customerCode).First();

                            try
                            {
                                // old: http://anc.astra.co.id/webapismsgw/api/json
                                using (var client = new HttpClient())
                                {
                                    var content = new StringContent("{\"PhoneNumber\":\"" + lead.CellNo.ToString() + "\",\"Message\":\"" + smsText.ToString() + "\",\"Tag\":\"CRMFocus\",\"Sender\":\"" + scenario.DealerCode + "-" + scenario.ScenarioCode + "\"}", Encoding.UTF8, "application/json");

                                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, SMSGatewayAPI);
                                    request.Content = content;
                                    var result = client.SendAsync(request).ContinueWith(responseTask => responseTask.Result.Content.ReadAsStringAsync().Result).Result;
                                    JObject deserializeResult = JObject.Parse(result);
                                    byte status = (bool) deserializeResult["Success"] == true ? (byte) 1 : (byte) 2;
                                    var token = (string) deserializeResult["Message"];

                                    var SMSFollowup = new SMSFollowup();
                                    SMSFollowup.Id = 1;
                                    SMSFollowup.SMSFollowupID = Guid.NewGuid().ToString().Substring(0, 5);
                                    SMSFollowup.CRMCustomerNum = customerCode;
                                    SMSFollowup.ScenarioCode = scenario.ScenarioCode;
                                    SMSFollowup.CellNo = lead.CellNo;
                                    SMSFollowup.SMSContent = smsText;
                                    SMSFollowup.Status = status;
                                    SMSFollowup.Senddate = DateTime.Now;
                                    SMSFollowup.Count = 1;
                                    SMSFollowup.CompanyCodeCode = null;
                                    SMSFollowup.isSync = 1;
                                    SMSFollowup.Token = token;
                                    SMSFollowup.isUpdate = 0;

                                    db.SMSFollowups.Add(SMSFollowup);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);

                                var SMSFollowup = new SMSFollowup();
                                SMSFollowup.Id = 1;
                                SMSFollowup.SMSFollowupID = Guid.NewGuid().ToString().Substring(0, 5);
                                SMSFollowup.CRMCustomerNum = customerCode;
                                SMSFollowup.ScenarioCode = scenario.ScenarioCode;
                                SMSFollowup.CellNo = lead.CellNo;
                                SMSFollowup.SMSContent = smsText;
                                SMSFollowup.Status = 0;
                                SMSFollowup.Senddate = DateTime.Now;
                                SMSFollowup.Count = 1;
                                SMSFollowup.CompanyCodeCode = null;
                                SMSFollowup.isSync = 0;
                                SMSFollowup.Token = null;
                                SMSFollowup.isUpdate = 0;

                                db.SMSFollowups.Add(SMSFollowup);
                                db.SaveChanges();
                            }

                            i++;
                        }
                    }

                }
            }
        }

    }
}
