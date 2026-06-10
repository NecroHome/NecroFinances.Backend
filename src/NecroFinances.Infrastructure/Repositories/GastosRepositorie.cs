using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Dtos;
using NecroFinances.Application.Enums;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using NecroFinances.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Infrastructure.Repositories
{
    public class GastosRepositorie : IGastosRepositorie
    {
        private readonly AppDbContext _context;

        public GastosRepositorie(
            AppDbContext context
            )
        {
            _context = context;
        }

        public async Task<long> GetLastSerie()
        {
            GastosModel lastSerie = await _context.Gastos
                .OrderByDescending(o => o.Serie)
                .FirstOrDefaultAsync();

            if (lastSerie == null)
            {
                return 1;
            }

            return lastSerie.Serie + 1;
        }

        public async Task<GastosModel> AddGasto(GastosModel model)
        {
            _context.Gastos.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteGasto(GastosModel model)
        {
            _context.Gastos.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GastosModel> GetGastoById(long gastoID, long userID)
        {
            return await _context.Gastos.FirstOrDefaultAsync(f => f.Id == gastoID && f.UserID == userID);
        }

        public async Task<GastosModel> UpdateGasto(GastosModel model)
        {
            _context.Gastos.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<GastosModel>> GetGastosByDate(DateOnly inicio, DateOnly fim, long userID)
        {
            List<GastosModel> list = await _context.Gastos
                .Where(w => w.DataGasto >= inicio && w.DataGasto <= fim && w.UserID == userID)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }
    }
}
