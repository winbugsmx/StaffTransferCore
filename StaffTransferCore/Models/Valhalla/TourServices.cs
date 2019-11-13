using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class TourServices
    {
        public int serviceReferenceId { get; set; }
        public string serviceNumber { get; set; }
        public int[] paxesReferences { get; set; }
        public int[] requirements { get; set; }
        public Tour tour { set; get; }
        public string tourDate { get; set; }
    }
}
