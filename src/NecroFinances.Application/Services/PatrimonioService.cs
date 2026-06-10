using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Helpers;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Services
{
    public class PatrimonioService : IPatrimonioService
    {
        public readonly IPatrimonioRepositorie _repositorie;
        public PatrimonioService(
            IPatrimonioRepositorie repositorie
            )
        {
            _repositorie = repositorie;
        }
        
        public async Task<PatrimonioDTO> GetPatrimonioByDate(DateOnly inicio, DateOnly fim, long userID)
        {
            PatrimonioModel model = await _repositorie.GetPatrimonioByDate(inicio, fim, userID);
            if (model == null)
            {
                model = new PatrimonioModel();
                model.Propriedades = new List<PropriedadeModel>();
                model.Investimentos = new List<InvestimentoModel>();
                model.Financiamentos = new List<FinanciamentoModel>();
                model.Data = new DateOnly(inicio.Year, inicio.Month, 10);
                model.UserID = userID;

                model = await _repositorie.AddPatrimonio(model);
                return new PatrimonioDTO(model);
            }
            return new PatrimonioDTO(model);
        }

        public async Task<PatrimonioDTO> UpdatePatrimonio(PatrimonioDTO dto, long userID)
        {
            PatrimonioModel model = await _repositorie.GetPatrimonioById(dto.Id, userID);

            if (model == null)
                throw new Exception("Patrimônio não encontrado");

            model.Data = dto.Data;

            List<PropriedadeModel> propriedadesAtuais = model.Propriedades;
            List<PropriedadeDTO> propriedadesNovas = dto.Propriedades;

            List<InvestimentoModel> investimentosAtuais = model.Investimentos;
            List<InvestimentoDTO> investimentosNovos = dto.Investimentos;

            List<FinanciamentoModel> financiamentosAtuais = model.Financiamentos;
            List<FinanciamentoDTO> financiamentosNovos = dto.Financiamentos;

            List<PropriedadeModel> propriedadesParaRemover = propriedadesAtuais
                .Where(atual =>
                    !propriedadesNovas.Any(nova => nova.Id == atual.Id)).ToList();

            List<InvestimentoModel> investimentosParaRemover = investimentosAtuais
                .Where(atual =>
                    !investimentosNovos.Any(nova => nova.Id == atual.Id)).ToList();

            List<FinanciamentoModel> financiamentosParaRemover = financiamentosAtuais
                .Where(atual =>
                    !financiamentosNovos.Any(nova => nova.Id == atual.Id)).ToList();

            await _repositorie.DeletePropriedadeList(propriedadesParaRemover);
            await _repositorie.DeleteInvestimentoList(investimentosParaRemover);
            await _repositorie.DeleteFinanciamentoList(financiamentosParaRemover);

            foreach (PropriedadeModel propriedadeAtual in propriedadesAtuais)
            {
                PropriedadeDTO dtoCorrespondente = propriedadesNovas
                    .FirstOrDefault(nova => nova.Id == propriedadeAtual.Id);

                if (dtoCorrespondente != null)
                {
                    propriedadeAtual.NomePropriedade = dtoCorrespondente.NomePropriedade;
                    propriedadeAtual.ValorPropriedade = dtoCorrespondente.ValorPropriedade;
                }
            }

            foreach (InvestimentoModel investimentoAtual in investimentosAtuais)
            {
                InvestimentoDTO dtoCorrespondente = investimentosNovos
                    .FirstOrDefault(nova => nova.Id == investimentoAtual.Id);

                if (dtoCorrespondente != null)
                {
                    investimentoAtual.NomeInvestimento = dtoCorrespondente.NomeInvestimento;
                    investimentoAtual.ValorInvestimento = dtoCorrespondente.ValorInvestimento;
                }
            }

            foreach (FinanciamentoModel financiamentoAtual in financiamentosAtuais)
            {
                FinanciamentoDTO dtoCorrespondente = financiamentosNovos
                    .FirstOrDefault(nova => nova.Id == financiamentoAtual.Id);

                if (dtoCorrespondente != null)
                {
                    financiamentoAtual.NomeFinanciamento = dtoCorrespondente.NomeFinanciamento;
                    financiamentoAtual.ValorFinanciamento = dtoCorrespondente.ValorFinanciamento;
                }
            }

            List<PropriedadeDTO> propriedadesParaAdicionar = propriedadesNovas
                .Where(nova => nova.Id == 0)
                .ToList();

            List<InvestimentoDTO> investimentosParaAdicionar = investimentosNovos
                .Where(nova => nova.Id == 0)
                .ToList();

            List<FinanciamentoDTO> financiamentosParaAdicionar = financiamentosNovos
                .Where(nova => nova.Id == 0)
                .ToList();

            foreach (PropriedadeDTO nova in propriedadesParaAdicionar)
            {
                PropriedadeModel novaEntidade = new PropriedadeModel
                {
                    NomePropriedade = nova.NomePropriedade,
                    ValorPropriedade = nova.ValorPropriedade,
                    PatrimonioId = model.Id
                };

                model.Propriedades.Add(novaEntidade);
            }

            foreach (InvestimentoDTO nova in investimentosParaAdicionar)
            {
                InvestimentoModel novaEntidade = new InvestimentoModel
                {
                    NomeInvestimento = nova.NomeInvestimento,
                    ValorInvestimento = nova.ValorInvestimento,
                    PatrimonioId = model.Id
                };

                model.Investimentos.Add(novaEntidade);
            }

            foreach (FinanciamentoDTO nova in financiamentosParaAdicionar)
            {
                FinanciamentoModel novaEntidade = new FinanciamentoModel
                {
                    NomeFinanciamento = nova.NomeFinanciamento,
                    ValorFinanciamento = nova.ValorFinanciamento,
                    PatrimonioId = model.Id
                };

                model.Financiamentos.Add(novaEntidade);
            }

            model = await _repositorie.UpdatePatrimonio(model);

            return new PatrimonioDTO(model);
        }
    }
}
