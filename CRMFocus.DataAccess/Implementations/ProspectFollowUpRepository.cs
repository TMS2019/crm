using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ProspectFollowUpRepository : BaseRepository<ProspectFollowUp>, IProspectFollowUpRepository
    {
        public ProspectFollowUpRepository(CRMFocusContext context) : 
            base(context)
        {
        }
    }
}
