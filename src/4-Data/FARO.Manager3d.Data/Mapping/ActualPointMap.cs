using System;
using FARO.Manager3d.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FARO.Manager3d.Data.Mapping
{
    public class ActualPointMap:IEntityTypeConfiguration<ActualPoint>
    {
        public void Configure(EntityTypeBuilder<ActualPoint> builder)
        {
            builder.ToTable("actual_point");

            builder.HasKey(a => a.Id);

            builder.Property<Guid>("nominal_point_id");

            builder.Ignore(a => a.Distance);

            builder
                .Property(a => a.X)
                .HasColumnType("float8");

            builder
                .Property(a => a.Y)
                .HasColumnType("float8");

            builder
                .Property(a => a.Z)
                .HasColumnType("float8");

            builder
                .HasOne(a => a.NominalPoint)
                .WithMany(n => n.ActualPoints)
                .HasForeignKey("nominal_point_id");

        }
    }
}