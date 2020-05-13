using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ScenarioLeadMapping : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(9)]
        public string LeadScenarioMappingCode { get; set; }

        [ForeignKey("Scenario")]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        public virtual Scenario Scenario { get; set; }

        [StringLength(9)]
        public string RefCampaignCode { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerNum { get; set; }

        public virtual Lead Lead { get; set; }

        [ForeignKey("LeadsUnitTransaction"), Column(Order = 6)]
        [StringLength(6)]
        public string EngineCode { get; set; }

        [ForeignKey("LeadsUnitTransaction"), Column(Order = 7)]
        [StringLength(8)]
        public string EngineNo { get; set; }

        public virtual LeadsUnitTransaction LeadsUnitTransaction { get; set; }

        //public int LeadsServiceId { get; set; }
        //public int LeadsSPId { get; set; }
    }
}
