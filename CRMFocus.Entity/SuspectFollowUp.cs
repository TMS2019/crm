using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class SuspectFollowUp : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(10)]
        public string SuspectFollowupID { get; set; }

        [ForeignKey("Suspect")]
        public string SuspectID { get; set; }

        public Suspect Suspect { get; set; }

        // Todo: Add foreign key
        [StringLength(50)]
        public string EmployeeID { get; set; }

        [ForeignKey("MasterStatus")]
        public int CallStatus { get; set; }

        public MasterStatus MasterStatus { get; set; }

        public DateTime FollowupDate { get; set; }

        public DateTime NextFollowupDate { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

    }
}
