using CRMFocus.Domain;
using CRMFocus.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Business.Interfaces
{
    public interface ILeadsService
    {
        bool Create(CreateLeadsView leadsView);
        List<CustomerProfileRef> GetAllCustomerProfileRef();
        List<Province> GetAllProvince();
        List<Kabupaten> GetAlKabupaten();
        List<Kecamatan> GetAllKecamatan();
        List<Kelurahan> GetAllKelurahan();
        List<UnityTypeMarket> GetUnityTypeMarket();
        List<LeadsTemporaryView> GetAllLeadsTemporary();
    }
}
