using NecroFinances.Application.Dtos;
using NecroFinances.Application.Models;
using System;

namespace NecroFinances.Application.Interfaces.Repositories
{
    public interface IPatrimonioRepositorie
    {
        Task<PatrimonioModel> GetPatrimonioByDate(DateTime inicio, DateTime fim, long userID);
        Task<PatrimonioModel> AddPatrimonio(PatrimonioModel model);
        Task<PatrimonioModel> GetPatrimonioById(long id, long userID);
        Task<PatrimonioModel> UpdatePatrimonio(PatrimonioModel model);
        Task DeletePropriedade(long id);
        Task DeletePropriedadeList(List<PropriedadeModel> list);
        Task DeleteInvestimento(long id);
        Task DeleteInvestimentoList(List<InvestimentoModel> list);
        Task DeleteFinanciamento(long id);
        Task DeleteFinanciamentoList(List<FinanciamentoModel> list);
    }
}
