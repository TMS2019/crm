using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ProspectTemporary : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int ProspectID { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerNum { get; set; }

        public Lead Lead { get; set; }

        [MaxLength(9)]
        public string ScenarioCode { get; set; }

        [MaxLength(10)]
        public string CurrentDealer { get; set; }

        [MaxLength(10)]
        public string CurrentSalesNo { get; set; }

        [MaxLength(50)]
        public string CurrentSalesName { get; set; }

        public byte ProspectStatus { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        [MaxLength(20)]
        public string MappingProspectCode { get; set; }

        public byte IsActive { get; set; }
    }
}
