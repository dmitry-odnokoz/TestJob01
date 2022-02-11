using System.Runtime.Serialization;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class EntityDuplicatedException: ApplicationCoreException {
    public EntityDuplicatedException(Guid enityId, string entityName) :
        base($"Entity {entityName} with id {enityId} duplicated") { }
    protected EntityDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
