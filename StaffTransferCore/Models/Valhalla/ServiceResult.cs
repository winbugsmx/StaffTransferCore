using StaffTransferCore.Models.Valhalla.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla
{
    public class ServiceResult
    {
        public ServiceResultEnum MyProperty { get; set; }
        public int code { get; set; }
        public string subCode { get; set; }
        public string message { get; set; }
    }
}
