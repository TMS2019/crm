using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioFilterRepository : BaseRepository<ScenarioFilter>, IScenarioFilterRepository
    {
        public ScenarioFilterRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
