using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Infrastructure.Persistence.Configuration
{
    public class FinanciamentoConfig : IEntityTypeConfiguration<FinanciamentoModel>
    {
        public void Configure(EntityTypeBuilder<FinanciamentoModel> builder)
        {
            builder.ToTable("Financiamentos");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(p => p.Patrimonio)
                .WithMany(p => p.Financiamentos)
                .HasForeignKey(p => p.PatrimonioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
