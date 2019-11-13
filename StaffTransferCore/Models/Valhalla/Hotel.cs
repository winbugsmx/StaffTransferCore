using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class Hotel
    {
        public int hotelReferenceId { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public int[] paxesReferences { get; set; }
        public int bedaCode { get; set; }
        public int giataCode { get; set; }
        public string name { get; set; }
    }
}
