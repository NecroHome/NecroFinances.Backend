using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class InvestimentoDTO
    {
        public long Id { get; set; }
        public long PatrimonioId { get; set; }
        public string NomeInvestimento { get; set; }
        public double ValorInvestimento { get; set; }
        public double Diferenca { get; set; }

        public InvestimentoDTO()
        {

        }

        public InvestimentoDTO(InvestimentoModel model)
        {
            Id = model.Id;
            PatrimonioId = model.PatrimonioId;
            NomeInvestimento = model.NomeInvestimento;
            ValorInvestimento = model.ValorInvestimento;
        }

        public static List<InvestimentoDTO> GenerateList(List<InvestimentoModel> list)
        {
            List<InvestimentoDTO> retorno = new List<InvestimentoDTO>();
            foreach (InvestimentoModel model in list)
            {
                retorno.Add(new InvestimentoDTO(model));
            }
            return retorno;
        }
    }
}
