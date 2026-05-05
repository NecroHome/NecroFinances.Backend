using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Infrastructure.Persistence.Configuration
{
    public class InvestimentoConfig : IEntityTypeConfiguration<InvestimentoModel>
    {
        public void Configure(EntityTypeBuilder<InvestimentoModel> builder)
        {
            builder.ToTable("Investimentos");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(p => p.Patrimonio)
                .WithMany(p => p.Investimentos)
                .HasForeignKey(p => p.PatrimonioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
