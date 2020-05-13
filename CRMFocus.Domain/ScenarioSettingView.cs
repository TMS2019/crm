using System;

namespace CRMFocus.Domain
{
    public class ScenarioSettingView
    {
        public string ScenarioSettingViewId { get; set; }
        public string ScenarioName { get; set; }
        public byte isAutomatic { get; set; }
        public string SMSContent { get; set; }
        public int SmsMax { get; set; }
        public string DataSortByDirection { get; set; }
        public DateTime StartDistributionSmsDate { get; set; }
        public DateTime EndDistributionSmsDate { get; set; }
        public byte isActive { get; set; }
    }
}
