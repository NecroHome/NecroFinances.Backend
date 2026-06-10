using NecroFinances.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<MainDataDTO> GetDashboard(DateOnly inicio, DateOnly fim, long userID);
    }
}
