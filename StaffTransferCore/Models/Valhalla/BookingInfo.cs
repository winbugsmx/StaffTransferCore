using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class BookingInfo
    {
        public string bookingNumber { set; get; }
        public string partnerCode { get; set; }
        public string externalReference { get; set; }
        public ArrivalFlight[] arrivalFlights { set; get; }
        public DepartureFlight[] departureFlights { set; get; }
        public Hotel[] hotels { set; get; }
        public Pax[] paxs { set; get; }
    }
}
