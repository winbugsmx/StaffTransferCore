using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class BookingServices
    {
        public ShuttleService[] shuttleServices { set; get; }
        public TourServices[] tourServices { set; get; }
    }
}
