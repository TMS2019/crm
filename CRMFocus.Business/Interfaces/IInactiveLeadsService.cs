using CRMFocus.Domain;
using System.Collections.Generic;

namespace CRMFocus.Business.Interfaces
{
    public interface IInactiveLeadsService
    {
        List<InactiveLeadsView> GetAllInactiveLeads();
        string ReactivateSuspect(string suspectIds);
        string DeleteSuspect(string suspectIds);
    }
}
