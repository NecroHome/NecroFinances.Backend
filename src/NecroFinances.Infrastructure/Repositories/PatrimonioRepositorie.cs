using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using NecroFinances.Infrastructure.Persistence;

namespace NecroFinances.Infrastructure.Repositories
{
    public class PatrimonioRepositorie : IPatrimonioRepositorie
    {
        private readonly AppDbContext _context;
        public PatrimonioRepositorie(
            AppDbContext context
            ) {
            _context = context;
        }

        public async Task<PatrimonioModel> GetPatrimonioByDate(DateOnly inicio, DateOnly fim, long userID)
        {
            PatrimonioModel model = await _context.Patrimonios
                .Include(i => i.Propriedades)
                .Include(i => i.Investimentos)
                .Include(i => i.Financiamentos)
                .Where(w => w.Data >= inicio && w.Data <= fim && w.UserID == userID)
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<PatrimonioModel> AddPatrimonio(PatrimonioModel model)
        {
            _context.Patrimonios.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<PatrimonioModel> GetPatrimonioById(long id, long userID)
        {
            return await _context.Patrimonios
                .Include(i => i.Propriedades)
                .Include(i => i.Investimentos)
                .Include(i => i.Financiamentos)
                .FirstOrDefaultAsync(f => f.Id == id && f.UserID == userID);
        }

        public async Task<PatrimonioModel> UpdatePatrimonio(PatrimonioModel model)
        {
            _context.Patrimonios.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeletePropriedade(long id)
        {
            PropriedadeModel model = await _context.Propriedades.FirstOrDefaultAsync(f => f.Id == id);
            _context.Propriedades.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePropriedadeList(List<PropriedadeModel> list)
        {
            foreach (PropriedadeModel p in list)
            {
                _context.Propriedades.Remove(p);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvestimento(long id)
        {
            InvestimentoModel model = await _context.Investimentos.FirstOrDefaultAsync(f => f.Id == id);
            _context.Investimentos.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvestimentoList(List<InvestimentoModel> list)
        {
            foreach(InvestimentoModel p in list)
            {
                _context.Investimentos.Remove(p);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFinanciamento(long id)
        {
            FinanciamentoModel model = await _context.Financiamentos.FirstOrDefaultAsync(f => f.Id == id);
            _context.Financiamentos.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFinanciamentoList(List<FinanciamentoModel> list)
        {
            foreach (FinanciamentoModel p in list)
            {
                _context.Financiamentos.Remove(p);
            }
            await _context.SaveChangesAsync();
        }
    }
}
