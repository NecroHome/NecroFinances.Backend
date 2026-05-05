using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NecroFinances.Application.Models;

public class PatrimonioConfig : IEntityTypeConfiguration<PatrimonioModel>
{
    public void Configure(EntityTypeBuilder<PatrimonioModel> builder)
    {
        builder.ToTable("Patrimonios");

        builder.HasKey(h => h.Id);
    }
}