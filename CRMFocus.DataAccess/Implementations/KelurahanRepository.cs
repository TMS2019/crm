using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class KelurahanRepository : BaseRepository<Kelurahan>, IKelurahanRepository
    {
        public KelurahanRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
