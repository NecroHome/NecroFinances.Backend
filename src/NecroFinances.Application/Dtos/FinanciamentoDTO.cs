using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class FinanciamentoDTO
    {
        public long Id { get; set; }
        public long PatrimonioId { get; set; }
        public string NomeFinanciamento { get; set; }
        public double ValorFinanciamento { get; set; }
        public double Diferenca { get; set; }

        public FinanciamentoDTO()
        {

        }

        public FinanciamentoDTO(FinanciamentoModel model)
        {
            Id = model.Id;
            PatrimonioId = model.PatrimonioId;
            NomeFinanciamento = model.NomeFinanciamento;
            ValorFinanciamento = model.ValorFinanciamento;
        }

        public static List<FinanciamentoDTO> GenerateList(List<FinanciamentoModel> list)
        {
            List<FinanciamentoDTO> retorno = new List<FinanciamentoDTO>();
            foreach (FinanciamentoModel model in list)
            {
                retorno.Add(new FinanciamentoDTO(model));
            }
            return retorno;
        }

    }
}
