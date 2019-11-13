using StaffTransferCore.Common;
using StaffTransferCore.Models.Valhalla;
using StaffTransferCore.Models.Valhalla.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffTransferCore.Service.Interfaces
{
    public interface IValhallaService
    {
        Task<AddServiceResponse> SendBooking(List<Booking> booking);
    }
}
