using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioHistoryRepository : BaseRepository<ScenarioHistory>, IScenarioHistoryRepository
    {
        public ScenarioHistoryRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
