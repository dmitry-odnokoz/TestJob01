
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.ValueObjects;

namespace TestJob01.Infrastructure.Data.Config;
public class RemainderConfiguration: IEntityTypeConfiguration<Remainder> {
    public void Configure(EntityTypeBuilder<Remainder> builder) {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property<byte[]>("RowVersion").IsRowVersion();
        builder.OwnsOne<RemainderQuantity>("Quantity");
        builder.HasKey(p => p.Id).IsClustered(false);
        builder.HasIndex("WarehouseId", "ProductId", "RemainderDate").IsUnique().IsClustered(true);
        builder.HasOne(p => p.Warehouse).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Product).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
}
