﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMFocus.Domain
{
    public class DistributeProspectView
    {
        public int ProspectId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LastPurchaseUnit { get; set; }
        public string LastDealer { get; set; }
        public string LastSales { get; set; }
        public string CurrentDealerCode { get; set; }
        public string CurrentDealer { get; set; }
        public string CurrentSales { get; set; }
        public string ExpireDate { get; set; }
    }
}
