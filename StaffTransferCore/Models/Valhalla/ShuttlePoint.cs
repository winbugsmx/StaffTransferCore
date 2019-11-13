using System;
using System.Collections.Generic;
using System.Text;
using StaffTransferCore.Models.Valhalla.Enums;

namespace StaffTransferCore.Models.Valhalla
{
    public class ShuttlePoint
    {
        public ShuttlePointType type { get; set; }
        public string code { get; set; }
        public int bedaCode { get; set; }
    }
}
