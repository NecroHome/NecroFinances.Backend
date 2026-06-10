using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Helpers;
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
    public class MesRepositorie : IMesRepositorie
    {
        private readonly AppDbContext _context;
        public MesRepositorie(
            AppDbContext context
            )
        {
            _context = context;
        }

        public async Task<MesModel> GetMesByDate(DateOnly inicio, DateOnly fim, long userID)
        {
            MesModel model = await _context.Meses
                .Where(w => w.Data >= inicio && w.Data <= fim && w.UserID == userID)
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<MesModel> GetLastMes()
        {
            MesModel model = await _context.Meses
                .OrderByDescending(o => o.Data)
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<MesModel> SaveMes(MesModel model)
        {
            _context.Meses.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MesModel> GetMesById(long id, long userID)
        {
            return await _context.Meses.FirstOrDefaultAsync(f => f.Id == id && f.UserID == userID);
        }

        public async Task<MesModel> UpdateMes(MesModel model)
        {
            _context.Meses.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
