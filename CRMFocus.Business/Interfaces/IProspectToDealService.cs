using CRMFocus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Business.Interfaces
{
    public interface IProspectToDealService
    {
        List<ProspectView> GetAllProspect();
    }
}
