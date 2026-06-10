using NecroFinances.Application.Dtos.Settings;

namespace NecroFinances.Application.Interfaces
{
    public interface IMesService
    {
        Task<MesDTO> GetMesByDate(DateOnly inicio, DateOnly fim, long userID);
        Task<MesDTO> UpdateMes(MesDTO dto, long userID);
    }
}
