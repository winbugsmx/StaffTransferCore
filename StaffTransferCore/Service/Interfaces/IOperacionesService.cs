using StaffTransferCore.Models.OperacionesService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffTransferCore.Service.Interfaces
{
    public interface IOperacionesService
    {
        Task<IEnumerable<LlegSalSTAFF>> GetLlegSalSTAFF();
        Task<IEnumerable<DetPaxSTAFF>> GetDetPaxSTAFF();
        Task<IEnumerable<ICamionSTAFF>> GetCamionSTAFF();
    }
}
