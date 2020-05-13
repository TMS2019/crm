using CRMFocus.Domain;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface ISuspectService
    {
        List<SuspectView> GetAllSuspects();
        string DeactivateSuspect(string[] suspectIds);
        string UpdateStatus(string suspectID, int callStatus, string nextFollowup, string note);
        void DeleteSuspect(string[] suspectIds);
        List<CallScriptView> GetCallScripts(string scenarioCode);
        List<SuspectCustomerView> GetDataCustomer(string suspectID);
        void SubmitAnswer(AnswerOfScriptView asv);
    }
}
