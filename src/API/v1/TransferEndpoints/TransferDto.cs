namespace TestJob01.API.v1.TransferEndpoints;
public record TransferDto: TransferHeadDto {
    public List<TransferItemDto> Items { get; init; } = new();
}
