using NecroFinances.Application.Dtos;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsDTO> GetSettingsByDate(DateOnly inicio, DateOnly fim, long userID);
        Task<SettingsDTO> UpdateSettings(SettingsDTO model, long userID);
    }
}
