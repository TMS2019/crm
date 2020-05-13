using CRMFocus.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Prospect : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public int ProspectID { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerNum { get; set; }

        public Lead Lead { get; set; }

        [ForeignKey("Scenario")]
        [MaxLength(9)]
        public string ScenarioCode { get; set; }

        public Scenario Scenario { get; set; }

        [StringLength(70)]
        public string LastDealerName { get; set; }

        [MaxLength(10)]
        public string LastSalesNo { get; set; }

        [MaxLength(50)]
        public string LastSalesName { get; set; }

        [ForeignKey("Dealer")]
        [StringLength(10)]
        public string CurrentDealerCode { get; set; }

        public Dealer Dealer { get; set; }

        [MaxLength(10)]
        public string CurrentSalesNo { get; set; }

        [MaxLength(50)]
        public string CurrentSalesName { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        [ForeignKey("Suspect")]
        [MaxLength(10)]
        public string SuspectID { get; set; }

        public Suspect Suspect { get; set; }

        public byte IsActive { get; set; }

        [MaxLength(4)]
        public string CompanyCodeCode { get; set; }

        public List<ProspectFollowUp> ProspectFollowUps { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
