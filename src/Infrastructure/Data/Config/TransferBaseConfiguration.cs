using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.Infrastructure.Data.Config;
public class TransferBaseConfiguration: IEntityTypeConfiguration<TransferBase> {
    public void Configure(EntityTypeBuilder<TransferBase> builder) {
        builder.ToTable("Transfers");
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion();
        builder.HasKey(p => p.Id)
            .IsClustered(false);
        builder.HasIndex("ShipperId", "ReceiverId", "OperationDate")
            .IsUnique()
            .IsClustered(true);
        builder.HasOne(p => p.Shipper)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Receiver)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(p => p.TransferItems)
            .WithOne()
            .HasForeignKey("TransferId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasDiscriminator<TransferKid>("Kid")
               .HasValue<Transfer>(TransferKid.Transfer)
               .HasValue<TransferStorno>(TransferKid.Storno);
    }
}
