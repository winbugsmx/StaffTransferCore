using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class Pax
    {
        public int paxReferenceId { get; set; }
        public string name { get; set; }
        public bool isAdult { get; set; }
        public bool isLead { get; set; }
        public int age { get; set; }
    }
}
