
using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class UnityTypeMarket : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string UnitTypeCode { get; set; }

        [StringLength(50)]
        public string UnitMarketNameCode { get; set; }

        [StringLength(50)]
        public string UnitMarketName { get; set; }

        public int UnitTypeSegmentID { get; set; }

        [StringLength(20)]
        public string UnitTypeSegment { get; set; }

        [StringLength(50)]
        public string UnitTypeSeries { get; set; }

        [Timestamp]
        [MaxLength(8)]
        public byte[] TimeStatus { get; set; }

        public short Rowstatus { get; set; }

        public int ETLBatchRunID { get; set; }
    }
}
