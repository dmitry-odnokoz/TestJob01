using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Services;
public class TransferService: ITransferService {
    private readonly HttpService _httpService;
    private readonly ILogger<TransferService> _logger;

    public TransferService(HttpService httpService,
        ILogger<TransferService> logger) {
        _httpService = httpService;
        _logger = logger;
    }

    public Task<bool> Create(CreateTransferRequest transfer) {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(DeleteTransferRequest request) {
        throw new NotImplementedException();
    }

    public async Task<List<TransferHead>> ListHeadsOnly() {
        _logger.LogInformation("Fetching transfer headers from API.");

        var result = await _httpService.HttpGet<ListTransferHeadsResponse>($"v1/transfers/head-only");
        return result?.TransferHeads ?? new();
    }
}


