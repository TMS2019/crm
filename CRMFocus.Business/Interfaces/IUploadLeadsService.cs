using CRMFocus.Domain;
using System.Collections.Generic;
using System.Web;

namespace CRMFocus.Business.Interfaces
{
    public interface IUploadLeadsService
    {
        List<UploadLeadsView> GetPreviewExcell(HttpFileCollectionBase files, string userRole);
        string SaveExcell(string[] customerCodes, string userRole);
    }
}
