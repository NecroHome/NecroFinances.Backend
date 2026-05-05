using NecroFinances.Application.Dtos;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Interfaces
{
    public interface IGastosService
    {
        Task<bool> AddGasto(GastosDTO dto, long userID);
        Task<GastosDTO> UpdateGasto(GastosDTO dto, long userID);
        Task<bool> DeleteGasto(long gastoId, long userID);
    }
}
