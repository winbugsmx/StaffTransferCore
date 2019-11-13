using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class Booking
    {
        public BookingInfo bookingInfo { set; get; }
        public BookingServices bookingServices { set; get; }
        public Booking()
        {
            bookingInfo = new BookingInfo();
            bookingServices = new BookingServices();
        }
    }
}
