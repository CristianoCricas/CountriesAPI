using CountriesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountriesAPI.Data.Map
{
    public class CountryMapping : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DateUpdated)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Alpha2Code)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.Alpha3Code)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.NumericCode)
                .IsRequired();

            builder.Property(x => x.IsoCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Independent)
                .IsRequired();
        }
    }
}
