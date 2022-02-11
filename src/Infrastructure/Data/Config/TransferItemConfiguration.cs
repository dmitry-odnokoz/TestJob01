using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestJob01.ApplicationCore.Entities.TransferAggregate;
using TestJob01.ApplicationCore.ValueObjects;

namespace TestJob01.Infrastructure.Data.Config;
public class TransferItemConfiguration: IEntityTypeConfiguration<TransferItem> {
    public void Configure(EntityTypeBuilder<TransferItem> builder) {
        // TODO бобавить уникальный кластерный индекс по TransferId, ProductId
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion();
        builder.HasKey(p => p.Id)
            .IsClustered(false);
        builder.HasIndex("TransferId", "ProductId")
            .IsUnique()
            .IsClustered(true);
        builder.HasOne(d => d.Product)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.OwnsOne<TransferQuantity>("Quantity");
    }
}
