using StaffTransferCore.Models.Valhalla.Entities;
using StaffTransferCore.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Configuration
{
    public class AppSettings
    {
        public OperacionesServiceApiClientOptions OperacionesServiceApiClientOptions { get; set; }
        public ValhallaApiClientOptions ValhallaApiClientOptions { get; set; }
        public AgencyData Agencydata { get; set; }
    }
}
