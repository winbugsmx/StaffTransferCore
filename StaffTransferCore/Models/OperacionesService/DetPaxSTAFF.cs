using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.OperacionesService
{
    public class DetPaxSTAFF
    {
        public int Clave_Det_Pax_Auto { get; set; }
        public int Clave_Lleg_Sal_auto { get; set; }
        public int Consec { get; set; }
        public string Lleg_Sal { get; set; }
        public string Hotel { get; set; }
        public int HotelBedaCode { get; set; }
        public string NombreHotel { get; set; }
        public int PersA { get; set; }
        public DateTime PickUp { get; set; }
        public string TipoServ { get; set; }
        public string Notas { get; set; }
    }
}