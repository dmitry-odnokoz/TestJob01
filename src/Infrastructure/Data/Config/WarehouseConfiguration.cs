using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.Infrastructure.Data.Config;
public class WarehouseConfiguration: IEntityTypeConfiguration<Warehouse> {
    public void Configure(EntityTypeBuilder<Warehouse> builder) {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property<byte[]>("RowVersion").IsRowVersion();
        builder.HasAlternateKey(p => p.Name);
        builder.Property(p => p.Name).HasMaxLength(255);
    }
}
