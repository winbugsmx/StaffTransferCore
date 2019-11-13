using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class ArrivalFlight
    {
        public string arrivalAirportIataCode { get; set; }
        public string arrivalDate { get; set; }
        public string arrivalTime { get; set; }
        public int flightReferenceId { set; get; }
        public string airlineIataCode { get; set; }
        public string flightNumber { get; set; }
        public string departureAirportIataCode { get; set; }
        public string departureDate { get; set; }
        public string departureTime { get; set; }
        public int[] paxesReferences { set; get; }
    }
}
