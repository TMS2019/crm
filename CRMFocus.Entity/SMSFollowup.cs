using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class SMSFollowup : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(10)]
        public string SMSFollowupID { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerNum { get; set; }

        public virtual Lead Lead { get; set; }

        [ForeignKey("Scenario")]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        public virtual Scenario Scenario { get; set; }

        [StringLength(50)]
        public string CellNo { get; set; }

        [StringLength(10)]
        public string SMSContent { get; set; }

        public byte Status { get; set; }

        public DateTime? Senddate { get; set; }

        public int Count { get; set; }

        [StringLength(4)]
        public string  CompanyCodeCode { get; set; }

        public byte isSync { get; set; }

        public string Token { get; set; }

        public byte isUpdate { get; set; }
    }
}
