using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Models;

namespace NecroFinances.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<SettingsModel> Settings { get; set; }
        public DbSet<MesModel> Meses { get; set; }
        public DbSet<GastosModel> Gastos { get; set; }
        public DbSet<PatrimonioModel> Patrimonios { get; set; }
        public DbSet<PropriedadeModel> Propriedades { get; set; }
        public DbSet<InvestimentoModel> Investimentos { get; set; }
        public DbSet<FinanciamentoModel> Financiamentos { get; set; }
        public DbSet<UserModel> Usuarios { get; set; }
    }
}
