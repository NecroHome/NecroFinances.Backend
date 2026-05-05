using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Dtos
{
    public class SettingsDTO
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public double ValorHora { get; set; }
        public double SalarioMinimo { get; set; }
        public double PercentagemTaxaINSS { get; set; }
        public double PercentagemTaxaCooperativa { get; set; }
        public double ValorPlanoDental { get; set; }
        public double ValorPlanoSaude { get; set; }
        public double DesafioGastos { get; set; }

        public SettingsDTO()
        {

        }

        public SettingsDTO(SettingsDTO baseSettings)
        {
            this.Id = baseSettings.Id + 1;
            this.Data = baseSettings.Data;
            this.ValorHora = baseSettings.ValorHora;
            this.SalarioMinimo = baseSettings.SalarioMinimo;
            this.PercentagemTaxaINSS = baseSettings.PercentagemTaxaINSS;
            this.PercentagemTaxaCooperativa = baseSettings.PercentagemTaxaCooperativa;
            this.ValorPlanoDental = baseSettings.ValorPlanoDental;
            this.ValorPlanoSaude = baseSettings.ValorPlanoSaude;
            this.DesafioGastos = baseSettings.DesafioGastos;
        }

        public SettingsDTO(SettingsModel model)
        {
            Id = model.Id;
            Data = model.Data;
            ValorHora = model.ValorHora;
            SalarioMinimo = model.SalarioMinimo;
            PercentagemTaxaINSS = model.PercentagemTaxaINSS;
            PercentagemTaxaCooperativa = model.PercentagemTaxaCooperativa;
            ValorPlanoDental = model.ValorPlanoDental;
            ValorPlanoSaude = model.ValorPlanoSaude;
            DesafioGastos = model.DesafioGastos;
        }

        public List<SettingsDTO> GenerateList(List<SettingsModel> list)
        {
            List<SettingsDTO> retorno = new List<SettingsDTO>();
            foreach (SettingsModel model in list)
            {
                retorno.Add(new SettingsDTO(model));
            }
            return retorno;
        }
    }
}
