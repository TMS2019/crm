using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    //public enum QuestionType
    //{
    //    ShortAnswer = 1,
    //    Paragrph = 2,
    //    RadioButton = 3
    //}

    public class Script : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        public int Scriptcode { get; set; }

        [StringLength(200)]
        public string Text { get; set; }

        public int RefCode { get; set; }

        public byte ScriptType { get; set; }

        public byte TypeQuestion { get; set; }

        public int NextQuestion  { get; set; }

        [ForeignKey("Scenario")]
        [StringLength(9)]
        public string ScenarioCode { get; set; }

        public virtual Scenario Scenario { get; set; }
    }
}
