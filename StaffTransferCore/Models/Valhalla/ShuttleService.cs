using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class ShuttleService
    {
        public int serviceReferenceId { get; set; }
        public string serviceNumber { get; set; }
        public int[] paxesReferences { get; set; }
        public int[] requirements { get; set; }
        public ShuttlePoint originShuttlePoint { set; get; }
        public ShuttlePoint destinationShuttlePoint { set; get; }
        public bool isPrivate { get; set; }
        public string shuttleDate { get; set; }
        public string pickupTime { get; set; }
    }
}
