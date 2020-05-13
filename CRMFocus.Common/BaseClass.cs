using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Common
{
    public class BaseClass
    {
        [Column(Order = 2)]
        public DateTime? CreatedTime { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(Order = 4)]
        public DateTime? LastModifiedTime { get; set; }

        [Column(Order = 5)]
        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string UserRole { get; set; }
    }
}
