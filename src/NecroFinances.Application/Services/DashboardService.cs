using NecroFinances.Application.Dtos;
using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Enums;
using NecroFinances.Application.Helpers;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IGastosRepositorie _gastosRepositorie;
        private readonly ISettingsService _settingsService;
        private readonly IMesService _mesService;
        private readonly IPatrimonioService _patrimonioService;
        public DashboardService(
            IGastosRepositorie gastosRepositorie,
            ISettingsService settingsService,
            IMesService mesService,
            IPatrimonioService patrimonioService
            )
        {
            _gastosRepositorie = gastosRepositorie;
            _settingsService = settingsService;
            _mesService = mesService;
            _patrimonioService = patrimonioService;
        }

        public async Task<MainDataDTO> GetDashboard(DateTime inicio, DateTime fim, long userID)
        {
            List<GastosModel> gastosMesAtual = await _gastosRepositorie.GetGastosByDate(inicio, fim, userID);
            List<GastosModel> gastosMesAnterior = await _gastosRepositorie.GetGastosByDate(inicio.AddMonths(-1), fim.AddMonths(-1), userID);

            SettingsDTO settingsMesAtual = await _settingsService.GetSettingsByDate(inicio, fim, userID);
            SettingsDTO settingsMesAnterior = await _settingsService.GetSettingsByDate(inicio.AddMonths(-1), fim.AddMonths(-1), userID);

            MesDTO mesAtual = await _mesService.GetMesByDate(inicio, fim, userID);
            MesDTO mesAnterior = await _mesService.GetMesByDate(inicio.AddMonths(-1), fim, userID);

            PatrimonioDTO patrimonioMesAtual = await _patrimonioService.GetPatrimonioByDate(inicio, fim, userID);
            PatrimonioDTO patrimonioMesAnterior = await _patrimonioService.GetPatrimonioByDate(inicio.AddMonths(-1), fim.AddMonths(1), userID);

            double totalBrutoMesAtual = (settingsMesAtual.ValorHora * (mesAtual.HorasUteis + mesAtual.HorasExtras));
            double totalBrutoMesAnterior = (settingsMesAnterior.ValorHora * (mesAnterior.HorasUteis + mesAnterior.HorasExtras));
            double diferencaBruto = totalBrutoMesAtual - totalBrutoMesAnterior;

            double inssMesAtual = settingsMesAtual.SalarioMinimo * settingsMesAtual.PercentagemTaxaINSS;
            double inssMesAnterior = settingsMesAnterior.SalarioMinimo * settingsMesAnterior.PercentagemTaxaINSS;
            double taxaMesAtual = totalBrutoMesAtual * settingsMesAtual.PercentagemTaxaCooperativa;
            double taxaMesAnterior = totalBrutoMesAnterior * settingsMesAnterior.PercentagemTaxaCooperativa;
            double pspdMesAtual = settingsMesAtual.ValorPlanoDental + settingsMesAtual.ValorPlanoSaude;
            double pspdMesAnterior = settingsMesAnterior.ValorPlanoDental + settingsMesAnterior.ValorPlanoSaude;

            double totalDescontosMesAtual = inssMesAtual + taxaMesAtual + pspdMesAtual;
            double totalDescontosMesAnterior = inssMesAnterior + taxaMesAnterior + pspdMesAnterior;
            double diferencaDescontos = totalDescontosMesAtual - totalDescontosMesAnterior;

            double totalLiquidoMesAtual = totalBrutoMesAtual - totalDescontosMesAtual;
            double totalLiquidoMesAnterior = totalBrutoMesAnterior - totalDescontosMesAnterior;
            double diferencaLiquido = totalLiquidoMesAtual - totalLiquidoMesAnterior;

            // ----------------
            List<PropriedadeDTO> propriedades = new List<PropriedadeDTO>();
            foreach (PropriedadeDTO atual in patrimonioMesAtual.Propriedades)
            {
                PropriedadeDTO propriedadeMesAnterior = patrimonioMesAnterior.Propriedades.FirstOrDefault(f => f.NomePropriedade == atual.NomePropriedade);
                propriedades.Add(new PropriedadeDTO
                {
                    Id = atual.Id,
                    NomePropriedade = atual.NomePropriedade,
                    ValorPropriedade = atual.ValorPropriedade,
                    Diferenca = propriedadeMesAnterior != null ? atual.ValorPropriedade - propriedadeMesAnterior.ValorPropriedade : 0
                });
            }

            List<InvestimentoDTO> investimentos = new List<InvestimentoDTO>();
            foreach (InvestimentoDTO atual in  patrimonioMesAtual.Investimentos)
            {
                InvestimentoDTO investimentoMesAnterior = patrimonioMesAnterior.Investimentos.FirstOrDefault(f => f.NomeInvestimento == atual.NomeInvestimento);
                investimentos.Add(new InvestimentoDTO
                {
                    Id = atual.Id,
                    NomeInvestimento = atual.NomeInvestimento,
                    ValorInvestimento = atual.ValorInvestimento,
                    Diferenca = investimentoMesAnterior != null ? atual.ValorInvestimento - investimentoMesAnterior.ValorInvestimento : 0
                });
            }

            List<FinanciamentoDTO> financiamentos = new List<FinanciamentoDTO>();
            foreach (FinanciamentoDTO atual in patrimonioMesAtual.Financiamentos)
            {
                FinanciamentoDTO financiamentoMesAnterior = patrimonioMesAnterior.Financiamentos.FirstOrDefault(f => f.NomeFinanciamento == atual.NomeFinanciamento);
                financiamentos.Add(new FinanciamentoDTO
                {
                    Id = atual.Id,
                    NomeFinanciamento = atual.NomeFinanciamento,
                    ValorFinanciamento = atual.ValorFinanciamento,
                    Diferenca = financiamentoMesAnterior != null ? atual.ValorFinanciamento - financiamentoMesAnterior.ValorFinanciamento : 0
                });
            }

            double economias = investimentos.Sum(s => s.ValorInvestimento);
            double totalPatrimonio = propriedades.Sum(s => s.ValorPropriedade) + economias - financiamentos.Sum(s => s.ValorFinanciamento);

            double totalPropriedadesMesAnterior = patrimonioMesAnterior.Propriedades.Sum(s => s.ValorPropriedade);
            double totalInvestimentosMesAnterior = patrimonioMesAnterior.Investimentos.Sum(s => s.ValorInvestimento);
            double totalFinanciamentosMesAnterior = patrimonioMesAnterior.Financiamentos.Sum(s => s.ValorFinanciamento);

            double economiasMesAnterior = totalInvestimentosMesAnterior;
            double totalPatrimonioMesAnterior = totalPropriedadesMesAnterior + totalInvestimentosMesAnterior - totalFinanciamentosMesAnterior;

            double diferencaEconomias = economias - economiasMesAnterior;
            double diferencaPatrimonio = totalPatrimonio - totalPatrimonioMesAnterior;

            // ----------------
            double totalGastosFixosMesAtual = gastosMesAtual.Where(w => w.TipoGasto == IndicadorTipoGasto.RECORRENTE).Sum(s => s.Valor);
            double totalGastosFixasMesAnterior = gastosMesAnterior.Where(w => w.TipoGasto == IndicadorTipoGasto.RECORRENTE).Sum(s => s.Valor);
            double diferencaGastosFixos = totalGastosFixosMesAtual - totalGastosFixasMesAnterior;

            double totalGastosParceladosMesAtual = gastosMesAtual.Where(w => w.TipoGasto == IndicadorTipoGasto.PARCELADO).Sum(s => s.Valor);
            double totalGastosParceladosMesAnterior = gastosMesAnterior.Where(w => w.TipoGasto == IndicadorTipoGasto.PARCELADO).Sum(s => s.Valor);
            double diferencaGastosParcelados = totalGastosParceladosMesAtual - totalGastosParceladosMesAnterior;

            double totalGastosAvulsosMesAtual = gastosMesAtual.Where(w => w.TipoGasto == IndicadorTipoGasto.AVULSO).Sum(s => s.Valor);
            double totalGastosAvulsosMesAnterior = gastosMesAnterior.Where(w => w.TipoGasto == IndicadorTipoGasto.AVULSO).Sum(s => s.Valor);
            double diferencaGastosAvulsos = totalGastosAvulsosMesAtual - totalGastosAvulsosMesAnterior;

            double totalGastosMesAtual = gastosMesAtual.Sum(s => s.Valor);
            double totalGastosMesAnterior = gastosMesAnterior.Sum(s => s.Valor);
            double diferencaGastos = totalGastosMesAtual - totalGastosMesAnterior;

            double diferencaMeta = settingsMesAtual.DesafioGastos - (totalGastosParceladosMesAtual + totalGastosAvulsosMesAtual);

            double totalRestanteMesAtual = totalLiquidoMesAtual - totalGastosMesAtual;
            double totalRestanteMesAnterior = totalLiquidoMesAnterior - totalGastosMesAnterior;
            double diferencaRestante = totalRestanteMesAtual - totalRestanteMesAnterior;

            MainDataDTO mainData = new MainDataDTO();
            mainData.totalBruto = totalBrutoMesAtual;
            mainData.diferencaBruto = diferencaBruto;

            mainData.totalDescontos = totalDescontosMesAtual;
            mainData.diferencaDescontos = diferencaDescontos;

            mainData.totalLiquido = totalLiquidoMesAtual;
            mainData.diferencaLiquido = diferencaLiquido;

            mainData.totalRestante = totalRestanteMesAtual;
            mainData.diferencaRestante = diferencaRestante;

            mainData.propriedades = propriedades.OrderByDescending(o => o.ValorPropriedade).ToList();
            mainData.investimentos = investimentos.OrderByDescending(o => o.ValorInvestimento).ToList();
            mainData.financiamentos = financiamentos.OrderByDescending(o => o.ValorFinanciamento).ToList();

            mainData.economias = economias;
            mainData.totalPatrimonio = totalPatrimonio;

            mainData.diferencaEconomias = diferencaEconomias;
            mainData.diferencaPatrimonio = diferencaPatrimonio;

            mainData.totalGastosFixos = totalGastosFixosMesAtual;
            mainData.diferencaGastosFixos = diferencaGastosFixos;
            mainData.totalGastosParcelados = totalGastosParceladosMesAtual;
            mainData.diferencaGastosParcelados = diferencaGastosParcelados;
            mainData.totalGastosAvulsos = totalGastosAvulsosMesAtual;
            mainData.diferencaGastosAvulsos = diferencaGastosAvulsos;

            mainData.diferencaMeta = diferencaMeta;

            mainData.listaGastosFixos = ConsolidarGastos(gastosMesAtual, gastosMesAnterior, IndicadorTipoGasto.RECORRENTE, totalLiquidoMesAtual);
            mainData.listaGastosParcelados = ConsolidarGastos(gastosMesAtual, gastosMesAnterior, IndicadorTipoGasto.PARCELADO, totalLiquidoMesAtual);
            mainData.listaGastosAvulsos = ConsolidarGastos(gastosMesAtual, gastosMesAnterior, IndicadorTipoGasto.AVULSO, totalLiquidoMesAtual);

            return mainData;
        }

        private List<DashboardDTO> ConsolidarGastos(
    List<GastosModel> mesAtual,
    List<GastosModel> mesAnterior,
    IndicadorTipoGasto tipoGasto,
    double totalGastosMesAtual)
        {
            var resultado = new List<DashboardDTO>();
            int id = 0;

            var atualFiltrado = mesAtual
                .Where(w => w.TipoGasto == tipoGasto)
                .ToList();

            var anteriorFiltrado = mesAnterior
                .Where(w => w.TipoGasto == tipoGasto)
                .ToList();

            // 🔥 PARCELADO OU RECORRENTE → NÃO AGRUPA
            if (tipoGasto == IndicadorTipoGasto.PARCELADO)
            {
                foreach (var item in atualFiltrado)
                {
                    double valorAnterior = 0;

                    // 🔹 PARCELADO → compara com parcela anterior
                    if (tipoGasto == IndicadorTipoGasto.PARCELADO)
                    {
                        var parcelaAnterior = item.Parcela - 1;

                        valorAnterior = anteriorFiltrado
                            .Where(x => x.Serie == item.Serie && x.Parcela == parcelaAnterior)
                            .Sum(x => x.Valor);
                    }
                    else
                    {
                        // 🔹 RECORRENTE → compara mesma "entidade"
                        valorAnterior = anteriorFiltrado
                            .Where(x =>
                                (x.Serie != 0 && x.Serie == item.Serie) ||
                                (x.Serie == 0 && x.Descricao == item.Descricao))
                            .Sum(x => x.Valor);
                    }

                    resultado.Add(new DashboardDTO
                    {
                        Id = id++,
                        Descricao = item.Descricao,
                        Categoria = item.Icone,
                        Valor = item.Valor,
                        Percentagem = totalGastosMesAtual > 0
                            ? (item.Valor / totalGastosMesAtual)
                            : 0,
                        Diferenca = item.Valor - valorAnterior,

                        Parcela = tipoGasto == IndicadorTipoGasto.PARCELADO ? item.Parcela : 0,
                        TotalParcelas = tipoGasto == IndicadorTipoGasto.PARCELADO ? item.TotalParcelas : 0,

                        Origem = new List<GastosDTO>
                {
                    new GastosDTO
                    {
                        Id = item.Id,
                        Serie = item.Serie,
                        DataGasto = item.DataGasto,
                        TipoGasto = item.TipoGasto,
                        Valor = item.Valor,
                        Icone = item.Icone,
                        Descricao = item.Descricao,
                        Parcela = item.Parcela,
                        TotalParcelas = item.TotalParcelas
                    }
                }
                    });
                }

                return resultado
                    .OrderByDescending(o => o.Percentagem)
                    .ToList();
            }

            // 🔹 OUTROS → AGRUPADO POR ICONE
            var atualAgrupado = atualFiltrado
                .GroupBy(g => g.Icone)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        Total = g.Sum(x => x.Valor),
                        Descricao = g.First().Descricao,
                        Icone = g.First().Icone,
                        Itens = g.Select(x => new GastosDTO
                        {
                            Id = x.Id,
                            Serie = x.Serie,
                            DataGasto = x.DataGasto,
                            TipoGasto = x.TipoGasto,
                            Valor = x.Valor,
                            Icone = x.Icone,
                            Descricao = x.Descricao,
                            Parcela = 0,
                            TotalParcelas = 0
                        }).ToList()
                    });

            var anteriorAgrupado = anteriorFiltrado
                .GroupBy(g => g.Icone)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(x => x.Valor)
                );

            var todasChaves = atualAgrupado.Keys
                .Union(anteriorAgrupado.Keys);

            foreach (var icone in todasChaves)
            {
                atualAgrupado.TryGetValue(icone, out var dadosAtual);
                anteriorAgrupado.TryGetValue(icone, out double valorAnterior);

                var valorAtual = dadosAtual?.Total ?? 0;

                resultado.Add(new DashboardDTO
                {
                    Id = id++,
                    Descricao = dadosAtual?.Descricao ?? "Sem descrição",
                    Categoria = icone,
                    Valor = valorAtual,
                    Percentagem = totalGastosMesAtual > 0
                        ? (valorAtual / totalGastosMesAtual)
                        : 0,
                    Diferenca = valorAtual - valorAnterior,

                    Parcela = 0,
                    TotalParcelas = 0,

                    Origem = dadosAtual?.Itens.OrderByDescending(o => o.Valor).ToList() ?? new List<GastosDTO>()
                });
            }

            return resultado
                .OrderByDescending(o => o.Percentagem)
                .ToList();
        }
    }
}
