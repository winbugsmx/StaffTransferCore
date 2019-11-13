using StaffTransferCore.Models.Valhalla.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla.Entities
{
    public class ShuttleResult
    {
        public string bookingNumber { get; set; }
        public string serviceNumber { get; set; }
        public string serviceCode { get; set; }
        public string bookingCode { get; set; }
        public ServiceOperation operation { get; set; }
        public ServiceResult serviceResult { get; set; }
    }
}
