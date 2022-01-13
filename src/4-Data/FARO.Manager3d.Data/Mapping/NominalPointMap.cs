using FARO.Manager3d.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FARO.Manager3d.Data.Mapping
{
    public class NominalPointMap : IEntityTypeConfiguration<NominalPoint>
    {
        public void Configure(EntityTypeBuilder<NominalPoint> builder)
        {

            builder.ToTable("nominal_point");

            builder.HasKey(n => n.Id);

            builder
                .Property(n=> n.X)
                .HasColumnType("float8");

            builder
                .Property(n=> n.Y)
                .HasColumnType("float8");

            builder
                .Property(n=> n.Z)
                .HasColumnType("float8");

            builder
                .HasMany(n => n.ActualPoints)
                .WithOne(a => a.NominalPoint);
                
            
        }
    }
}