using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.OperacionesService
{
    public class LlegSalSTAFF
    {
        public int Clave_LLeg_Sal_auto { get; set; }
        public string Clave_Ag { get; set; }
        public string Nombre { get; set; }
        public string Vuelo_Llega { get; set; }
        public DateTime? Hora_Llega { get; set; }
        public string Vuelo_Sale { get; set; }
        public DateTime? Hora_Sale { get; set; }
        public string Observaciones { get; set; }
        public string ConfirmAG { get; set; }
        public string Clave_Aero { get; set; }
        public string Clave_AeroIATA { get; set; }
        public bool Activo { get; set; }
    }
}
