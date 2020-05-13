using CRMFocus.DataAccess;
using CRMFocus.Entity;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace CRMFocus.Business.Implementations
{
    public class LeadJobService
    {
        static readonly log4net.ILog logger = LogManager.GetLogger("logUploadLeadDWAI");
       
        public static void Delete3MonthsDataInTemporaryTable(int count)
        {
            using (var db = new CRMFocusContext())
            {

                var leadsUnitTransactionList = db.LeadsUnitTransactionTemporaries.ToList();
                var leadsUnitTransactionTemporaryMoreThan3Months = leadsUnitTransactionList.Where(w => DateTime.Now.AddMonths(-count) > w.CreatedTime).ToList();
                foreach (var item in leadsUnitTransactionTemporaryMoreThan3Months)
                {
                    db.LeadsUnitTransactionTemporaries.Remove(item);
                    db.SaveChanges();
                }


                var leadsList = db.LeadsTemporaries.ToList();
                var leadsMoreThan3Months = leadsList.Where(w => DateTime.Now.AddMonths(-count) > w.CreatedTime).ToList();
                foreach (var item in leadsMoreThan3Months)
                {
                    var leadUnitTransactionTemporaryTemp = db.LeadsUnitTransactionTemporaries.Where(w => w.LeadsTemporaryId == item.Id).FirstOrDefault();
                    if (leadUnitTransactionTemporaryTemp == null)
                    {
                        db.LeadsTemporaries.Remove(item);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void UploadTemporaryLeadEvery3HrToDWAI(string baseAddress, Dictionary<string, string> credentials)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var lead = GetLeadsTemporary();
                    if (lead != "")
                    {
                        var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(credentials)).Result;
                        var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseAddress + "/api/staging/PutDataToDWAI");

                        request.Content = new StringContent("{\"TCode\":\"CRMLeads\",\"TData\":[\"" + lead + "\"],\"Origin\":\"Origin\",\"SysId\":\"SysId\"}", Encoding.UTF8, "application/json");
                        var result = client.SendAsync(request).ContinueWith(responseTask => responseTask.Result.Content.ReadAsStringAsync().Result).Result;

                        var message = new Dictionary<string, string>()
                        {
                            {"Lead:", lead},
                            {"Result:", result}
                        };

                        logger.Info(message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            
        }

        public static string GetLeadsTemporary()
        {
            var builder = new System.Text.StringBuilder();
            using (var db = new CRMFocusContext())
            {
                // createtime createby  modifytime modifyby   ID card           customerCode    Birthdate   Address                   Gender  Religion  Profesion  Spending  Education  Name     CellNo       isCallable  Email             CRMCustomerNum
                //"42839;     test;     42839;     Someone;   3322136309840003; I;              30948;      Rambai RT/ RW: 009 / 004; 2;      1;        2;         2;        4;         test;    2470807074;   Y;         test4@gmail.com;  888888"
                
                int index = 0;              
                var leadsTemporaries = db.LeadsTemporaries.ToList();
                foreach (var item in leadsTemporaries)
                {
                    if (item.CreatedTime.HasValue)
                    {
                        builder.Append(item.CreatedTime.Value.Ticks).Append(";");
                    }
                    else
                    {
                        builder.Append(item.CreatedTime).Append(";");
                    }
                   
                    builder.Append(item.CreatedBy).Append(";");

                    if (item.LastModifiedTime.HasValue)
                    {
                        builder.Append(item.LastModifiedTime.Value.Ticks).Append(";");
                    }
                    else
                    {
                        builder.Append(item.LastModifiedTime).Append(";");
                    }
                                      
                    builder.Append(item.LastModifiedBy).Append(";");
                    builder.Append(item.IDCardNo).Append(";");
                    builder.Append(item.CustomerCode).Append(";");

                    if (item.BirthDate.HasValue)
                    {
                        builder.Append(item.BirthDate.Value.Ticks).Append(";");
                    }
                    else
                    {
                        builder.Append(item.BirthDate).Append(";");
                    }


                    builder.Append(item.Address).Append(";");
                    builder.Append(item.Gender).Append(";");
                    builder.Append(item.Religion).Append(";");
                    builder.Append(item.Profesion).Append(";");
                    builder.Append(item.Spending).Append(";");
                    builder.Append(item.Education).Append(";");
                    builder.Append(item.Name).Append(";");
                    builder.Append(item.CellNo).Append(";");
                    builder.Append(item.isCallable).Append(";");
                    builder.Append(item.Email).Append(";");
                    builder.Append(item.Id).Append(";");   // is CRM Customer number

                    index++;
                    if (index < leadsTemporaries.Count)
                    {
                        builder.Append(",");
                    }
                }

            }

            return builder.ToString();
        }
    }
   

    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
 