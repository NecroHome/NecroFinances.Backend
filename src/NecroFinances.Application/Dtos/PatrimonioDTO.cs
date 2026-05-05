using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class PatrimonioDTO
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public List<PropriedadeDTO> Propriedades { get; set; }
        public List<InvestimentoDTO> Investimentos { get; set; }
        public List<FinanciamentoDTO> Financiamentos { get; set; }

        public PatrimonioDTO()
        {

        }

        public PatrimonioDTO(PatrimonioModel model)
        {
            Id = model.Id;
            Propriedades = PropriedadeDTO.GenerateList(model.Propriedades);
            Investimentos = InvestimentoDTO.GenerateList(model.Investimentos);
            Financiamentos = FinanciamentoDTO.GenerateList(model.Financiamentos);
            Data = model.Data;
        }

        public static List<PatrimonioDTO> GenerateList(List<PatrimonioModel> list)
        {
            List<PatrimonioDTO> retorno = new List<PatrimonioDTO>();
            foreach (var item in list)
            {
                retorno.Add(new PatrimonioDTO(item));
            }
            return retorno;
        }
    }
}
