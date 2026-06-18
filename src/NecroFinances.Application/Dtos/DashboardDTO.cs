using NecroFinances.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class DashboardDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public double Valor { get; set; }
        public double Percentagem { get; set; }
        public double Diferenca { get; set; }
        public int Parcela { get; set; }
        public int TotalParcelas { get; set; }
        public List<GastosDTO> Origem { get; set; } = new();
        public IndicadorTipoRecurso TipoRecurso { get; set; }
    }
}
