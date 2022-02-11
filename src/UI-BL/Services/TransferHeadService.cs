using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Services;
public class TransferHeadService: ITransferHeadService {
    private readonly HttpService _httpService;
    private readonly ILogger<TransferHeadService> _logger;

    public TransferHeadService(HttpService httpService,
        ILogger<TransferHeadService> logger) {
        _httpService = httpService;
        _logger = logger;
    }
    public async Task<List<TransferHead>> List() {
        _logger.LogInformation("Fetching transfer headers from API.");

        var result = await _httpService.HttpGet<ListTransferHeadsResponse>($"v1/transfers/head-only");
        return result?.TransferHeads ?? new();
    }
}
