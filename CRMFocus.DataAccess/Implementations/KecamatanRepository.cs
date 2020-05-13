using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class KecamatanRepository : BaseRepository<Kecamatan>, IKecamatanRepository
    {
        public KecamatanRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
