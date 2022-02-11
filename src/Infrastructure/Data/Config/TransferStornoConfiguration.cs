using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.Infrastructure.Data.Config;
internal class TransferStornoConfiguration: IEntityTypeConfiguration<TransferStorno> {
    public void Configure(EntityTypeBuilder<TransferStorno> builder) {
        builder.HasIndex(p => p.OriginalTransferId)
            .IsUnique()
            .HasFilter("[OriginalTransferId] is not null");
    }
}
