
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TestJob01.ApplicationCore.Constants;
using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.Infrastructure.Data;
public static class TestJob01ContextSeed {
    public static async Task SeedAsync(TestJob01Context dbContext,
        ILogger logger) {
        try {
            if (await dbContext.Products.AnyAsync()
                || await dbContext.Warehouses.AnyAsync()
                || await dbContext.Remainders.AnyAsync()
                || await dbContext.Transfers.AnyAsync())
                return;

            logger.LogInformation("Start seeding test data...");

            Random rnd = new Random();
            var startDate = new DateTimeOffset(new DateTime(2021, 12, 31));
            var endDate = new DateTimeOffset(new DateTime(2022, 02, 1));
            var secondsInInterval = (int) endDate.Subtract(startDate).TotalSeconds;

            var products = Enumerable.Range(1, 10).Select(i => new Product(Guid.NewGuid(), $"Продукт {i:D2}")).ToList();
            var wareHouses = Enumerable.Range(1, 10).Select(i => new Warehouse(Guid.NewGuid(), $"Склад {i:D2}")).ToList();

            var remainders = new List<Remainder>();
            for (int i = 0; i < products.Count / 2 * wareHouses.Count / 2; i++) {
                var product = products[rnd.Next(0, products.Count - 1)];
                var wareHouse = wareHouses[rnd.Next(0, wareHouses.Count - 1)];
                if (!remainders.Any(x => x.Warehouse.Id == wareHouse.Id && x.Product.Id == product.Id)) {
                    var remainderQuantity = rnd.Next(0, QuantityConstants.MAX_REMAINDER_QUANTITY);
                    remainders.Add(new Remainder(Guid.NewGuid(), wareHouse, product, remainderQuantity, startDate));
                }
            }

            dbContext.Warehouses.AddRange(wareHouses);
            dbContext.Remainders.AddRange(remainders);
            dbContext.Products.AddRange(products);

            for (int i = 0; i < 10; i++) {
                var trasferDate = startDate.AddSeconds(rnd.Next(0, secondsInInterval));
                var shipperIndex = rnd.Next(0, wareHouses.Count - 1);
                var recieverIndex = rnd.Next(0, wareHouses.Count - 1);
                recieverIndex = (recieverIndex != shipperIndex) ? recieverIndex : (recieverIndex + 1) % wareHouses.Count;
                var shipper = wareHouses[shipperIndex];
                var reciever = wareHouses[recieverIndex];
                var shipperRemainders = remainders.Where(x => x.Warehouse.Id == shipper.Id && x.Quantity.Value > 0)
                    .Select(x => x).ToList();
                var itemsCount = rnd.Next(1, shipperRemainders.Count);
                var transferItems = new List<TransferItem>();
                var transferRemainders = new List<Remainder>();
                for (int j = 0; j < itemsCount; j++) {
                    var shipperRemainder = shipperRemainders[rnd.Next(0, shipperRemainders.Count - 1)];
                    var product = shipperRemainder.Product;
                    if (!transferItems.Any(x => x.Product.Id == product.Id)) {
                        var quantity = Math.Min(rnd.Next(1, QuantityConstants.MAX_TRANSFER_QUANTITY),
                            shipperRemainder.Quantity.Value - 1);
                        transferItems.Add(new TransferItem(Guid.NewGuid(), product, quantity));
                        transferRemainders.Add(new Remainder(Guid.NewGuid(), shipper, product,
                            shipperRemainder.Quantity.Value - quantity, trasferDate));
                        var receiverRemainder = remainders.FirstOrDefault(x => x.Product.Id == product.Id
                            && x.Warehouse.Id == reciever.Id);
                        transferRemainders.Add(new Remainder(Guid.NewGuid(), reciever, product,
                            (receiverRemainder?.Quantity.Value ?? 0) + quantity, trasferDate));
                    }
                }
                dbContext.Transfers.Add(new Transfer(Guid.NewGuid(), shipper, reciever, trasferDate, transferItems));
                dbContext.Remainders.AddRange(transferRemainders);
            }

            await dbContext.SaveChangesAsync();
            logger.LogInformation("Test data seeded.");
        } catch (Exception ex) {
            logger.LogError(ex, "SeedAsync");
        }
    }
}