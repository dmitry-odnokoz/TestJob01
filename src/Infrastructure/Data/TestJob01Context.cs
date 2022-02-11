using System.Reflection;

using Microsoft.EntityFrameworkCore;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.Infrastructure.Data {
    public class TestJob01Context: DbContext {
        public TestJob01Context(DbContextOptions<TestJob01Context> options) : base(options) {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Remainder> Remainders => Set<Remainder>();
        public DbSet<Transfer> Transfers => Set<Transfer>();
        public DbSet<TransferItem> TransferItems => Set<TransferItem>();
        public DbSet<TransferStorno> TransferStorno => Set<TransferStorno>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            // TODO Перенести из конфигураций сущностей отключение генерации ключа
            // TODO Перенести из конфигураций сущностей добавление RowVersion
            // TODO Настроить точность DateTimeOffset
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
