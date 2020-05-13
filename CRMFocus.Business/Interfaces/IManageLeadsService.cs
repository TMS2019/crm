using CRMFocus.Domain;
using CRMFocus.Entity;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IManageLeadsService 
    {
        List<ManageLeadsView> GetAllLeads();
        string UpdateSuspectDealer(string suspectIds, string currentDealer);
        List<Dealer> GetDealerDropDown();
    }
}
 