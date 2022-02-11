using System.Runtime.Serialization;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class TransferWithoutItemsException: ApplicationCoreException {
    public TransferWithoutItemsException(Guid transferId) :
        base($"Transfer with id {transferId} has no items",
        "В перемещении должны быть продуты!") { }
    protected TransferWithoutItemsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
