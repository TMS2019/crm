using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class LeadsUnitTransactionTemporary : BaseClass
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [StringLength(50)]
        public string UnitMarketName { get; set; }

        [StringLength(6)]
        public string EngineCode { get; set; }

        [StringLength(8)]
        public string EngineNo { get; set; }

        public DateTime? TglBeli { get; set; }

        [StringLength(1)]
        public string PaymentType { get; set; }

        public string ServiceType { get; set; }

        public string SourceData { get; set; }

        public DateTime? SeviceDate { get; set; }

        [StringLength(50)]
        public string Kelurahan { get; set; }

        [StringLength(50)]
        public string Kecamatan { get; set; }

        [StringLength(50)]
        public string Kabupaten { get; set; }

        [StringLength(50)]
        public string Provinsi { get; set; }

        [StringLength(20)]
        public string UnitTypeSegment { get; set; }

        [StringLength(50)]
        public string UnitTypeSeries { get; set; }

        [StringLength(4)]
        public string MainDealerCode { get; set; }

        [StringLength(50)]
        public string MainDealerName { get; set; }

        [StringLength(10)]
        public string DealerCode { get; set; }

        [StringLength(70)]
        public string DealerName { get; set; }

        [ForeignKey("LeadsTemporary")]
        public int LeadsTemporaryId { get; set; }

        public LeadsTemporary LeadsTemporary { get; set; }
    }
}
