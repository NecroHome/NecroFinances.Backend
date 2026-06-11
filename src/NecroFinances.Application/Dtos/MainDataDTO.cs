using NecroFinances.Application.Dtos.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Dtos
{
    public class MainDataDTO
    {
        public double totalBruto { get; set; }
        public double diferencaBruto { get; set; }
        
        public double totalDescontos { get; set; }
        public double diferencaDescontos { get; set; }

        public double totalLiquido { get; set; }
        public double diferencaLiquido { get; set; }

        public double totalRestante { get; set; }
        public double diferencaRestante { get; set; }

        public double totalINSS { get; set; }
        public double diferencaINSS { get; set; }

        public double totalIRPF { get; set; }
        public double diferencaIRPF { get; set; }

        public List<PropriedadeDTO> propriedades { get; set; }
        public List<InvestimentoDTO> investimentos { get; set; }
        public List<FinanciamentoDTO> financiamentos { get; set; }

        public double economias { get; set; }
        public double diferencaEconomias { get; set; }
        public double totalPatrimonio { get; set; }
        public double diferencaPatrimonio { get; set; }

        public double totalGastosFixos { get; set; }
        public double diferencaGastosFixos { get; set; }
        public double totalGastosParcelados { get; set; }
        public double diferencaGastosParcelados { get; set; }
        public double totalGastosAvulsos { get; set; }
        public double diferencaGastosAvulsos { get; set; }

        public double valorMeta { get; set; }
        public double diferencaMeta { get; set; }

        public List<DashboardDTO> listaGastosFixos { get; set; }
        public List<DashboardDTO> listaGastosParcelados { get; set; }
        public List<DashboardDTO> listaGastosAvulsos { get; set; }
    }
}
