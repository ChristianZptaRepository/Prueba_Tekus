using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nit)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(p => p.Nit).IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CustomAttributes)
                .HasConversion(
                    dict => JsonSerializer.Serialize(dict, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, (JsonSerializerOptions?)null)
                            ?? new Dictionary<string, string>()
                )
                .HasColumnType("nvarchar(max)");
        }
    }
}
