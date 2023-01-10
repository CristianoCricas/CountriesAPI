using CountriesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace CountriesAPI.Data.Map
{
    public class CountrySubdivisionMapping : IEntityTypeConfiguration<CountrySubdivisionEntity>
    {
        /// <summary>
        /// Mapping the SubdivisionEntity to EntityFramework
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<CountrySubdivisionEntity> builder)
        {
            builder.ToTable("Subdivision");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DateUpdated)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Category)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.SubCode)
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}
