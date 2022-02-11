using System.Runtime.Serialization;

namespace TestJob01.ApplicationCore.Exceptions;
[Serializable]
public class ApplicationCoreException: Exception {
    private string? _uiMessage;

    public string UIMessage { get => _uiMessage ?? Message; set => _uiMessage = value; }
    public ApplicationCoreException(string message) : base(message) { }
    public ApplicationCoreException(string message, string uiMessage) : base(message) => UIMessage = uiMessage;
    protected ApplicationCoreException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
