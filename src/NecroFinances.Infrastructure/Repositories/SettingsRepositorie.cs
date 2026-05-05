using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using NecroFinances.Infrastructure.Persistence;

namespace NecroFinances.Infrastructure.Repositories
{
    public class SettingsRepositorie : ISettingsRepositorie
    {
        private readonly AppDbContext _context;
        public SettingsRepositorie(
            AppDbContext context
            ) 
        {
            _context = context;
        }

        public async Task<SettingsModel> GetSettingsByDate(DateTime inicio, DateTime fim, long userID)
        {
            SettingsModel model = await _context.Settings
                .Where(w => w.Data >= inicio && w.Data <= fim && w.UserID == userID)
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<SettingsModel> GetLastSettings(long userID)
        {
            SettingsModel model = await _context.Settings
                .Where(w => w.UserID == userID)
                .OrderByDescending(w => w.Data)
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<SettingsModel> SaveSettings(SettingsModel model)
        {
            _context.Settings.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SettingsModel> UpdateSettings(SettingsModel model)
        {
            _context.Settings.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SettingsModel> GetSettingsById(long id, long userID)
        {
            return await _context.Settings.FirstOrDefaultAsync(f => f.Id == id && f.UserID == userID);
        }
    }
}
