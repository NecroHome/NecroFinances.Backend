using NecroFinances.Application.Dtos.Settings;

namespace NecroFinances.Application.Interfaces
{
    public interface IMesService
    {
        Task<MesDTO> GetMesByDate(DateTime inicio, DateTime fim, long userID);
        Task<MesDTO> UpdateMes(MesDTO dto, long userID);
    }
}
