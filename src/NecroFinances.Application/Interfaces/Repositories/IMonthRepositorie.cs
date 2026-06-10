using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Interfaces.Repositories
{
    public interface IMesRepositorie
    {
        Task<MesModel> GetMesByDate(DateOnly inicio, DateOnly fim, long userID);
        Task<MesModel> GetLastMes();
        Task<MesModel> SaveMes(MesModel mes);
        Task<MesModel> GetMesById(long id, long userID);
        Task<MesModel> UpdateMes(MesModel model);
    }
}
