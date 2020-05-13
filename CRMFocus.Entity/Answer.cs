using CRMFocus.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Answer : BaseClass
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [StringLength(10)]
        public string ScenarioAnswerCode { get; set; }

        public int CustomerCode { get; set; }

        [StringLength(9)]
        public string ScenarioCode { get; set; }

        [StringLength(10)]
        public string ScenerioScriptMappingCode { get; set; }

        public int? IDAsk { get; set; }
        public int? IDAnswer { get; set; }

        [StringLength(200)]
        public string Handling { get; set; }

    }
}
