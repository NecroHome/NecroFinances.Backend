using NecroFinances.Application.Dtos;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Interfaces
{
    public interface IPatrimonioService
    {
        Task<PatrimonioDTO> GetPatrimonioByDate(DateTime inicio, DateTime fim, long userID);
        Task<PatrimonioDTO> UpdatePatrimonio(PatrimonioDTO dto, long userID);
    }
}
