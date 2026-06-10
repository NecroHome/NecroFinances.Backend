using NecroFinances.Application.Enums;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Dtos
{
    public class GastosDTO
    {
        public long? Id { get; set; }
        public long Serie { get; set; }
        public DateOnly DataGasto { get; set; }
        public IndicadorTipoGasto TipoGasto { get; set; }
        public double Valor { get; set; }
        public string Icone { get; set; }
        public string Descricao { get; set; }
        public int Parcela { get; set; }
        public int TotalParcelas { get; set; }

        public GastosDTO()
        {

        }

        public GastosDTO(GastosModel model)
        {
            Id = model.Id;
            Serie = model.Serie;
            DataGasto = model.DataGasto;
            TipoGasto = model.TipoGasto;
            Valor = model.Valor;
            Descricao = model.Descricao;
            Parcela = model.Parcela;
            Icone = model.Icone;
            TotalParcelas = model.TotalParcelas;
        }

        public static List<GastosDTO> GenerateList(List<GastosModel> list)
        {
            List<GastosDTO> retorno = new List<GastosDTO>();
            foreach (GastosModel model in list)
            {
                retorno.Add(new GastosDTO(model));
            }
            return retorno;
        }
    }
}
