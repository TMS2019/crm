using CRMFocus.Common;
using CRMFocus.DataAccess.Interfaces;
using CRMFocus.Entity;

namespace CRMFocus.DataAccess.Implementations
{
    public class ScenarioSettingRepository : BaseRepository<ScenarioSetting>, IScenarioSettingRepository
    {
        public ScenarioSettingRepository(CRMFocusContext context)
            : base(context)
        {
        }
    }
}
