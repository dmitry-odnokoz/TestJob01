using System.Runtime.Serialization;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class RemainderTooSmallException: ApplicationCoreException {
    public RemainderTooSmallException(Warehouse warehouse, Product product) :
        base($"Warehouse {warehouse.Id} has not reamainder for product {product.Id}",
        $"На складе \"{warehouse.Name }\" отсутсвует \"{product.Name}\"") { }
    public RemainderTooSmallException(Warehouse warehouse, Product product,
        int requiredQuantity, int availableQuantity) :
        base(@$"Warehouse {warehouse.Id} has too small reamainder for product {product.Id}.
 {nameof(requiredQuantity)} = {requiredQuantity}, {nameof(availableQuantity)} = {availableQuantity}",
             $"На складе \"{warehouse.Name }\" есть только {availableQuantity} \"{product.Name}\". Затребовано {requiredQuantity}.") { }
    protected RemainderTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
