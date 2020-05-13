using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class DummyRepository : BaseRepository<Dummy>, IDummyRepository
    {
        public DummyRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
