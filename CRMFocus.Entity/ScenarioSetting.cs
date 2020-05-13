using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ScenarioSetting : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public string ScenarioSettingCode { get; set; }

        [ForeignKey("Scenario")]
        public string ScenarioCode { get; set; }

        public virtual Scenario Scenario { get; set; }

        [ForeignKey("Dealer")]
        public string DealerCode { get; set; }

        public virtual Dealer Dealer { get; set; }

        [ForeignKey("Sms")]
        public string SmsCode { get; set; }

        public virtual Sms Sms { get; set; }

        public byte isAutomatic { get; set; }

        public byte isActive { get; set; }

        public int MaxSms { get; set; }

        public string DataSortByDirection { get; set; }

        public DateTime StartDistributionSmsDate { get; set; }

        public DateTime EndDistributionSmsDate { get; set; }
    }
}
