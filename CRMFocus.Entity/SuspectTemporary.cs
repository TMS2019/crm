using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class SuspectTemporary : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int SuspectID { get; set; }

        [ForeignKey("LeadsTemporary")]
        public int CRMCustomerNum { get; set; }

        public LeadsTemporary LeadsTemporary { get; set; }

        [ForeignKey("Scenario")]
        [MaxLength(9)]
        public string ScenarioCode { get; set; }

        public Scenario Scenario { get; set; }

        [MaxLength(50)]
        public string LastPurchaseUnit { get; set; }

        [MaxLength(10)]
        public string LastDealerName { get; set; }

        [MaxLength(10)]
        public string LastSalesNo { get; set; }

        [MaxLength(50)]
        public string LastSalesName { get; set; }

        [MaxLength(10)]
        public string CurrentDealer { get; set; }

        [MaxLength(10)]
        public string CurrentSalesNo { get; set; }

        [MaxLength(50)]
        public string CurrentSalesName { get; set; }

        //to do add master foreign key here
        public int SuspectStatus { get; set; }

        [MaxLength(4)]
        public string CompanyCodeCode { get; set; }
    }
}
