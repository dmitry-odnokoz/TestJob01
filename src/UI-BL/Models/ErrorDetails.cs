using System.Text.Json;

namespace TestJob01.UI_BL.Models;
public record struct ErrorDetails(int StatusCode, string Message) {
    public override string ToString() {
        return JsonSerializer.Serialize(this);
    }
}
