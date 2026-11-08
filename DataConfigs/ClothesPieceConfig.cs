using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataConfigs;

public class ClothesPieceConfig : IEntityTypeConfiguration<ClothesPiece>
{
    public void Configure(EntityTypeBuilder<ClothesPiece> entity)
    {
        entity.HasKey(p => p.Id);
        entity.HasIndex(p => p.Name);
        entity.Property(p => p.Name).IsRequired();
        entity.Property(p => p.Desc).IsRequired();
        entity.Property(p => p.Category).IsRequired().HasConversion<string>();
        entity.Property(p => p.Image).IsRequired();
        entity.Property(p => p.DownloadLink).IsRequired();
    }
}