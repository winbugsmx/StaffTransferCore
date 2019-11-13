using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla.Entities
{
    public class AddServiceResponse
    {
        public IEnumerable<ErrorBooking> errors;
        public IEnumerable<ShuttleResult> shuttleResults;
    }
}
