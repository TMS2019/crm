using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Scenario : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        [StringLength(200)]
        public string ScenarioName { get; set; }

        [ForeignKey("Campaign")]
        public int? RefCampaignCode { get; set; }

        public virtual Campaign Campaign { get; set; }

        public byte isDefault { get; set; }

        [StringLength(200)]
        public string ScenarioDescription { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public byte ResourceType { get; set; }

        public byte DestinationType { get; set; }

        public byte isSMS { get; set; }

        public byte isCall { get; set; }

        public DateTime StartDistributionSMSDate { get; set; }

        public DateTime EndDistributionSMSDate { get; set; }

        [ForeignKey("Dealer")]
        [StringLength(10)]
        public string DealerCode { get; set; }

        public virtual Dealer Dealer { get; set; }

        [ForeignKey("Employee")]
        public int? SubmitionEmployeCode { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey("ScenarioFilter")]
        [StringLength(20)]
        public string MappingFillerCode { get; set; }

        public virtual ScenarioFilter ScenarioFilter { get; set; }

        [ForeignKey("ScenarioHistory")]
        [StringLength(20)]
        public string MappingHistoryCode { get; set; }

        public virtual ScenarioHistory ScenarioHistory { get; set; }

        [ForeignKey("MasterStatus")]
        public int? StatusCode { get; set; }

        public virtual MasterStatus MasterStatus { get; set; }

        [StringLength(4)]
        public string CompanyCodeCode { get; set; }

        public string SmsContent { get; set; }
    }
}
