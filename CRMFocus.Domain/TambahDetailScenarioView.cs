using System;
using System.ComponentModel.DataAnnotations;

namespace CRMFocus.Domain
{
    public class TambahDetailScenarioView
    {       
        [StringLength(200)]
        public string ScenarioName { get; set; }
                
        public DateTime StartDate { get; set; }
              
        public DateTime EndDate { get; set; }
              
        public DateTime StartDistributionSMSDate { get; set; }
               
        public DateTime EndDistributionSMSDate { get; set; }

        [StringLength(200)]
        public string Note { get; set; }
    }
}
