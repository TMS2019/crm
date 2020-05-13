using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ProspectFollowUp : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public int ProspectFollowupID { get; set; }

        [ForeignKey("Prospect")]
        public int ProspectID { get; set; }

        public Prospect Prospect { get; set; }

        public byte ProspectStatus { get; set; }

        public DateTime? FollowupDate { get; set; }

        public DateTime? NextFollowupDate { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }
    }
}
