using CRMFocus.Domain;
using CRMFocus.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Business.Interfaces
{
    public interface IDistributeProspectService
    {
        List<Dealer> GetAllDealer();
        List<Employee> GetSalesByDealer(string dealerCode);
        List<DistributeProspectView> GetSelectedProspect(string prospectIds);
        bool DistributeProspect(List<DistributeProspectView> prospectViews, string dealer);
    }
}
