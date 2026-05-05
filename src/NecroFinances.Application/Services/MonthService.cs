using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Helpers;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Services
{
    public class MesService : IMesService
    {
        private readonly IMesRepositorie _repositorie;
        public MesService(
            IMesRepositorie repositorie
            )
        {
            _repositorie = repositorie;
        }
        public async Task<MesDTO> GetMesByDate(DateTime inicio, DateTime fim, long userID)
        {
            MesModel mes = await _repositorie.GetMesByDate(inicio, fim, userID);
            if (mes == null)
            {
                mes = await GenerateNewModel(inicio, userID);
            }
            return new MesDTO(mes);
        }

        private async Task<MesModel> GenerateNewModel(DateTime inicio, long userID)
        {
            MesModel novoMes = new MesModel();

            DateTime data = DiasHorasUteisHelper.AjustarParaDia(inicio, 10);
            novoMes.Data = data;
            novoMes.DiasUteis = DiasHorasUteisHelper.CalcularDiasUteis(data);
            novoMes.HorasUteis = novoMes.DiasUteis * 8;
            novoMes.HorasExtras = 0;
            novoMes.UserID = userID;

            novoMes = await _repositorie.SaveMes(novoMes);
            return novoMes;
        }

        public async Task<MesDTO> UpdateMes(MesDTO dto, long userID)
        {
            MesModel model = await _repositorie.GetMesById(dto.Id, userID);
            model.DiasUteis = dto.DiasUteis;
            model.HorasUteis = dto.HorasUteis;
            model.HorasExtras = dto.HorasExtras;

            model = await _repositorie.UpdateMes(model);
            return new MesDTO(model);
        }
    }
}
