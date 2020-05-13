using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioScriptMappingRepository : BaseRepository<ScenarioScriptMapping>, IScenarioScriptMappingRepository
    {
        public ScenarioScriptMappingRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
