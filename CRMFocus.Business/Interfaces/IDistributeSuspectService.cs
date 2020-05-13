using CRMFocus.Domain;
using CRMFocus.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Business.Interfaces
{
    public interface IDistributeSuspectService
    {
        List<Dealer> GetAllDealer();
        List<Employee> GetSalesByDealer(string dealerCode);
        List<DistributeSuspectView> GetSelectedSuspect(string prospectIds);
        DistributeSuspectView[] UpdateSuspects(DistributeSuspectView[] updatedSales);
    }
}
