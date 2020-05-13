using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ScenarioFilter : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string FillerCode { get; set; } 

        [StringLength(50)]
        public string TargetCustumerName { get; set; }

        public string OdataQueryScript { get; set; }
    }
}
