using NecroFinances.Application.Dtos;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Interfaces.Repositories
{
    public interface ISettingsRepositorie
    {
        Task<SettingsModel> GetSettingsByDate(DateTime inicio, DateTime fim, long userID);
        Task<SettingsModel> GetLastSettings(long userID);
        Task<SettingsModel> SaveSettings(SettingsModel model);
        Task<SettingsModel> UpdateSettings(SettingsModel model);
        Task<SettingsModel> GetSettingsById(long id, long userID);
    }
}
