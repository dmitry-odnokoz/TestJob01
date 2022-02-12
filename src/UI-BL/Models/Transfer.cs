namespace TestJob01.UI_BL.Models;
public record Transfer: TransferHead {
    public List<TransferItem> Items { get; init; } = new();
}
