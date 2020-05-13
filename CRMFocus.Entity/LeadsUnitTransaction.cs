using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class LeadsUnitTransaction : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Timestamp]
        [MaxLength(8)]
        public byte[] TimeStatus { get; set; }

        public short RowStatus { get; set; }

        public DateTime SourceSystemCreatedTime { get; set; }

        [StringLength(50)]
        public string SourceSystemCreatedBy { get; set; }

        public DateTime SourceSystemLastModifiedTime { get; set; }

        [StringLength(50)]
        public string SourceSystemLastModifiedBy { get; set; }

        [ForeignKey("Lead")]
        public int CRMCustomerCode { get; set; }

        public Lead Lead { get; set; }

        [Key, Column(Order = 6)]
        [StringLength(6)]
        public string EngineCode { get; set; }

        [Key, Column(Order = 7)]
        [StringLength(8)]
        public string EngineNo { get; set; }

        public int ItemNo { get; set; }

        [StringLength(50)]
        public string SourceSystem { get; set; }

        [StringLength(50)]
        public string SourceData { get; set; }

        [StringLength(1)]
        public string BikeUsage { get; set; }

        [StringLength(1)]
        public string BikeUser { get; set; }

        [StringLength(1)]
        public string PaymentType { get; set; }

        public DateTime TglBeliDLR { get; set; }

        [StringLength(4)]
        public string MainDealerCode { get; set; }

        [StringLength(4)]
        public string UnitTypeCode { get; set; }

        [StringLength(4)]
        public string UnitVariantCode { get; set; }

        [StringLength(50)]
        public string UnitMarketName { get; set; }

        [StringLength(10)]
        public string SalesPersonNo { get; set; }

        public int MappingBPSvsCDDBID { get; set; }

        [StringLength(50)]
        public string HondaID { get; set; }

        [StringLength(10)]
        public string DealerCode { get; set; }

        [StringLength(70)]
        public string DealerName { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Telp1 { get; set; }

        public byte iSActive { get; set; }

        [StringLength(50)]
        public string MainDealerName { get; set; }

        public byte IsHSO { get; set; }

        [StringLength(50)]
        public string KelurahanCode { get; set; }

        [StringLength(50)]
        public string Kelurahan { get; set; }

        [StringLength(50)]
        public string Kecamatan { get; set; }

        [StringLength(50)]
        public string KabupatenCode { get; set; }

        [StringLength(50)]
        public string Kabupaten { get; set; }

        [StringLength(50)]
        public string ProvinceCode { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string KecamatanCode { get; set; }

        public int HSOID { get; set; }

        [StringLength(20)]
        public string UnitTypeSegment { get; set; }

        [StringLength(50)]
        public string UnitTypeSeries { get; set; }

        [StringLength(4)]
        public string CompanyCodeCode { get; set; }
    }
}
