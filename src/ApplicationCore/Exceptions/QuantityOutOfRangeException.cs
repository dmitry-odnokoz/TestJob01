using System.Runtime.Serialization;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class QuantityOutOfRangeException: ApplicationCoreException {
    public QuantityOutOfRangeException(int quantity, int min, int max)
        : base($"Quantity {quantity} out of range {min}-{max}",
            $"Кол-во {quantity} не попадает в диапазон {min}-{max}") {
    }
    protected QuantityOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
