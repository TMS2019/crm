using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ScenarioHistory : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string MappingHistoryCode { get; set; }
               
        public byte? StatusCode { get; set; }

        [ForeignKey("RejectedByEmployee")]
        public int? RejectedBy  { get; set; }
        public virtual Employee RejectedByEmployee { get; set; }

        public int? SubmitionEmployeCode { get; set; }

        [StringLength(200)]
        public string RejectReason { get; set; }

        [ForeignKey("ApprovedByEmployee")]
        public int? ApprovedBy { get; set; }
        public virtual Employee ApprovedByEmployee { get; set; }

        public DateTime? Date { get; set; }
    }
}
