using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class KabupatenRepository : BaseRepository<Kabupaten>, IKabupatenRepository
    {
        public KabupatenRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
