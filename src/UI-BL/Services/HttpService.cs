using System.Text;
using System.Text.Json;

namespace TestJob01.UI_BL.Services;
public class HttpService {
    private readonly HttpClient _httpClient;
    private readonly ToastService _toastService;

    public HttpService(HttpClient httpClient, ToastService toastService) {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    public async Task<T> HttpGet<T>(string uri)
        where T : class {
        var result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode) {
            return null;
        }

        return await FromHttpResponseMessage<T>(result);
    }

    public async Task<T> HttpDelete<T>(string uri, int id)
        where T : class {
        var result = await _httpClient.DeleteAsync($"{uri}/{id}");
        if (!result.IsSuccessStatusCode) {
            return null;
        }

        return await FromHttpResponseMessage<T>(result);
    }

    public async Task<T> HttpPost<T>(string uri, object dataToSend)
        where T : class {
        var content = ToJson(dataToSend);

        var result = await _httpClient.PostAsync($"{uri}", content);
        if (!result.IsSuccessStatusCode) {
            //var exception = JsonSerializer.Deserialize<ErrorDetails>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions {
            //    PropertyNameCaseInsensitive = true
            //});
            //_toastService.ShowToast($"Error : {exception.Message}", ToastLevel.Error);

            return null;
        }

        return await FromHttpResponseMessage<T>(result);
    }

    public async Task<T> HttpPut<T>(string uri, object dataToSend)
        where T : class {
        var content = ToJson(dataToSend);

        var result = await _httpClient.PutAsync($"{uri}", content);
        if (!result.IsSuccessStatusCode) {
            //_toastService.ShowToast("Error", ToastLevel.Error);
            return null;
        }

        return await FromHttpResponseMessage<T>(result);
    }

    private StringContent ToJson(object obj) {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    private async Task<T> FromHttpResponseMessage<T>(HttpResponseMessage result) {
        return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });
    }
}
