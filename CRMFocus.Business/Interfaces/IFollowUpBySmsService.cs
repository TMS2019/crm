using CRMFocus.Domain;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IFollowUpBySmsService
    {
        List<FollowUpBySmsView> GetAllFollowUpBySms();
        List<FollowUpBySmsDetailsView> GetAllDetailsFollowUpBySms(string scenarioCode);
    }
}
