using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class ScenarioScriptMapping : BaseClass
    {
        [Column(Order = 1)]
        public int? Id { get; set; }

        [Key]
        [StringLength(10)]
        public string ScenarioScriptMappingCode { get; set; }

        public int Squence { get; set; }

        [ForeignKey("Script")]
        public int ScriptCode { get; set; }

        public virtual Script Script { get; set; }

        [ForeignKey("Scenario")]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        public virtual Scenario Scenario { get; set; }

        public byte? TransactionID { get; set; }
        public byte? TransactionType { get; set; }
    }

}
