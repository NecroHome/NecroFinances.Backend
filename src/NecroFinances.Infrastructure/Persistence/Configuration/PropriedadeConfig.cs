using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NecroFinances.Application.Models;

public class PropriedadeConfig : IEntityTypeConfiguration<PropriedadeModel>
{
    public void Configure(EntityTypeBuilder<PropriedadeModel> builder)
    {
        builder.ToTable("Propriedades");

        builder.HasKey(x => x.Id);

        builder
            .HasOne(p => p.Patrimonio)
            .WithMany(p => p.Propriedades)
            .HasForeignKey(p => p.PatrimonioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}