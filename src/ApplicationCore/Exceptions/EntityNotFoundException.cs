using System.Runtime.Serialization;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class EntityNotFoundException: ApplicationCoreException {
    public EntityNotFoundException(Guid enityId, string entityName) :
        base($"No {entityName} found with id {enityId}") { }
    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
