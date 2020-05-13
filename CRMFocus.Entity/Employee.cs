using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Employee : BaseClass
    {
        [Column(Order = 1)]
        public int ID { get; set; }

        [Key]
        public int CRMID { get; set; }

        public int? MainDealerID { get; set; }

        [StringLength(4)]
        public string MainDealerCode { get; set; }

        public int? DealerID { get; set; }

        [StringLength(10)]
        public string DealerCode { get; set; }

        public int POSID { get; set; }

        [StringLength(10)]
        public string POSCode { get; set; }

        [StringLength(50)]
        public string HSOID { get; set; }

        [StringLength(50)]
        public string HondaID { get; set; }

        [StringLength(50)]
        public string NPK { get; set; }

        public int TeamLeaderID { get; set; }

        public int SalesmanStatusUOMID { get; set; }
        
        [StringLength(100)]
        public string EmployeeStatus { get; set; } 

        [StringLength(70)]
        public string Name { get; set; }

        public int IsSalesActive { get; set; }

        public int PositionID { get; set; }

        [StringLength(100)]
        public string EmployeePosition { get; set; }

        public DateTime JoinDate { get; set; }

        public int IsInactiveBySystem   { get; set; }

        [Timestamp]
        [MaxLength(8)]
        public byte[] TimeStatus { get; set; }

        public short RowStatus { get; set; }

        public int ETLBatchRunID { get; set; }
    }
}
