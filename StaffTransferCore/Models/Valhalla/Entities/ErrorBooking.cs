using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla.Entities
{
    public class ErrorBooking
    {
        public string bookingNumber { get; set; }
        public int code { get; set; }
        public string subCode { get; set; }
        public string message { get; set; }
        public SeverityError severityError { get; set; }
    }
}
