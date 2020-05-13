using CRMFocus.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMFocus.Entity
{
    public class Sms : BaseClass
    {
        [Column(Order= 1)]
        public int Id { get; set; }

        [Key]
        public string SmsCode { get; set; }

        [ForeignKey("Scenario")]
        public string ScenarioCode { get; set; }
        
        public virtual Scenario Scenario { get; set; }

        public string SmsContent { get; set; }

        public byte isDefault { get; set; }

        public string DealerCode { get; set; }

        public int MainDealerCode { get; set; }
    }
}
