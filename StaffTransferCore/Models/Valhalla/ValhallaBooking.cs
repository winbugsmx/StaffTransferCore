using StaffTransferCore.Models.Valhalla.Entities;
using StaffTransferCore.Models.Valhalla.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class ValhallaBooking
    {

        public string PartnerCode { set; get; }
        public string BookingId { set; get; }
        public string ExternalReference { get; set; }
        public IEnumerable<Guest> Guests { set; get; }
        public ArrivalInfo Arrival { set; get; }
        public DepartureInfo Departure { set; get; }
        public ArrivalFlightInfo ArrivalFlightInfo { set; get; }
        public DepartureFlightInfo DepartureFlightInfo { set; get; }
        public TransportationTypeEnum TransportationType { get; set; }
        public IEnumerable<Shuttle> Shuttles { get; set; }
    }
}
