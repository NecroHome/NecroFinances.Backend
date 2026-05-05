using NecroFinances.Application.Dtos;
using NecroFinances.Application.Helpers;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettingsRepositorie _repository;
        public SettingsService(
            ISettingsRepositorie repositorie
            ) 
        {
            _repository = repositorie;
        }
        
        public async Task<SettingsDTO> GetSettingsByDate(DateTime inicio, DateTime fim, long userID)
        {
            SettingsModel model = await _repository.GetSettingsByDate(inicio, fim, userID);
            if (model == null)
            {
                model = await GenerateNewModel(inicio, userID);
            }
            return new SettingsDTO(model);
        }

        private async Task<SettingsModel> GenerateNewModel(DateTime inicio, long userID)
        {
            SettingsModel baseModel = await _repository.GetLastSettings(userID);
            if (baseModel == null)
            {
                baseModel = new SettingsModel();
                baseModel.Data = DiasHorasUteisHelper.AjustarParaDia(inicio, 10);
                baseModel.ValorHora = 0;
                baseModel.SalarioMinimo = 0;
                baseModel.PercentagemTaxaINSS = 0;
                baseModel.PercentagemTaxaCooperativa = 0;
                baseModel.ValorPlanoDental = 0;
                baseModel.ValorPlanoSaude = 0;
                baseModel.DesafioGastos = 0;
                baseModel.UserID = userID;

                baseModel = await _repository.SaveSettings(baseModel);
                return baseModel;
            }

            SettingsModel novoModel = new SettingsModel();
            novoModel.Data = DiasHorasUteisHelper.AjustarParaDia(inicio, 10);
            novoModel.ValorHora = baseModel.ValorHora;
            novoModel.SalarioMinimo = baseModel.SalarioMinimo;
            novoModel.PercentagemTaxaINSS = baseModel.PercentagemTaxaINSS;
            novoModel.PercentagemTaxaCooperativa = baseModel.PercentagemTaxaCooperativa;
            novoModel.ValorPlanoDental = baseModel.ValorPlanoDental;
            novoModel.ValorPlanoSaude = baseModel.ValorPlanoSaude;
            novoModel.DesafioGastos = baseModel.DesafioGastos;
            novoModel.UserID = userID;

            novoModel = await _repository.SaveSettings(novoModel);
            return novoModel;
        }

        public async Task<SettingsDTO> UpdateSettings(SettingsDTO dto, long userID)
        {
            SettingsModel model = await _repository.GetSettingsById(dto.Id, userID);

            model.ValorHora = dto.ValorHora;
            model.SalarioMinimo = dto.SalarioMinimo;
            model.PercentagemTaxaINSS = dto.PercentagemTaxaINSS;
            model.PercentagemTaxaCooperativa = dto.PercentagemTaxaCooperativa;
            model.ValorPlanoDental = dto.ValorPlanoDental;
            model.ValorPlanoSaude = dto.ValorPlanoSaude;
            model.DesafioGastos = dto.DesafioGastos;

            model = await _repository.UpdateSettings(model);
            return new SettingsDTO(model);
        }
    }
}
