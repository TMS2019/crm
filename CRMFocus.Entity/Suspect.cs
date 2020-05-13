using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Suspect : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(10)]
        public string SuspectID { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerNum { get; set; }

        public Lead Lead { get; set; }

        [ForeignKey("Scenario")]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        public Scenario Scenario { get; set; }

        [StringLength(50)]
        public string LastPurchaseUnit { get; set; }

        [StringLength(10)]
        public string LastDealerName { get; set; }

        [StringLength(10)]
        public string LastSalesNo { get; set; }

        [StringLength(50)]
        public string LastSalesName { get; set; }

        [StringLength(10)]
        public string CurrentDealer { get; set; }

        [StringLength(10)]
        public string CurrentSalesNo { get; set; }

        [StringLength(50)]
        public string CurrentSalesName { get; set; }

        [ForeignKey("MasterStatus")]
        public int SuspectStatus { get; set; }

        public MasterStatus MasterStatus { get; set; }

        [StringLength(4)]
        public string CompanyCodeCode { get; set; }

        public DateTime? LastReactivate { get; set; }

        public bool IsInactive { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
