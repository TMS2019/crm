using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioRepository : BaseRepository<Scenario>, IScenarioRepository
    {
        public ScenarioRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
