using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.OperacionesService
{
    public class ICamionSTAFF
    {
        public int Clave_Camion { get; set; }
        public int Clave_Lleg_sal_auto { get; set; }
        public int Clave_det_pax_auto { get; set; }
        public int Clav_Bus { get; set; }
        public int Cla_Ope { get; set; }
        public string Placa { get; set; }
        public int Chofer { get; set; }
        public int Pax_Asignados { get; set; }
        public int Capacidad { get; set; }
    }
}
