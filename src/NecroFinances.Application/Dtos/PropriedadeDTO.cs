using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class PropriedadeDTO
    {
        public long Id { get; set; }
        public long PatrimonioId { get; set; }
        public string NomePropriedade { get; set; }
        public double ValorPropriedade { get; set; }
        public double Diferenca { get; set; }

        public PropriedadeDTO()
        {

        }

        public PropriedadeDTO (PropriedadeModel model)
        {
            Id = model.Id;
            PatrimonioId = model.PatrimonioId;
            NomePropriedade = model.NomePropriedade;
            ValorPropriedade = model.ValorPropriedade;
        }

        public static List<PropriedadeDTO> GenerateList(List<PropriedadeModel> list)
        {
            List<PropriedadeDTO> retorno = new List<PropriedadeDTO>();
            foreach (PropriedadeModel model in list)
            {
                retorno.Add(new PropriedadeDTO(model));
            }
            return retorno;
        }
    }
}
