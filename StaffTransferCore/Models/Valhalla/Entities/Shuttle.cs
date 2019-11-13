using StaffTransferCore.Models.Valhalla.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla.Entities
{
    public class Shuttle
    {
        public int ID { get; set; }
        public TransportationTypeEnum TransportationType { get; set; }
        public ShuttleTypeEnum ShuttleType { get; set; }
        public int[] PaxIds { get; set; }
    }
}
