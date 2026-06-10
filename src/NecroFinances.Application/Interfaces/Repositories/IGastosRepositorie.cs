using NecroFinances.Application.Dtos;
using NecroFinances.Application.Enums;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Interfaces.Repositories
{
    public interface IGastosRepositorie
    {
        Task<long> GetLastSerie();
        Task<GastosModel> AddGasto(GastosModel model);
        Task<bool> DeleteGasto(GastosModel model);
        Task<GastosModel> GetGastoById(long gastoId, long userID);
        Task<GastosModel> UpdateGasto(GastosModel model);
        Task<List<GastosModel>> GetGastosByDate(DateOnly inicio, DateOnly fim, long userID);
    }
}
