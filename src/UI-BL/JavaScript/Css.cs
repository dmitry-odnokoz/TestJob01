using Microsoft.JSInterop;

namespace TestJob01.UI_BL.JavaScript;
public class Css {
    private readonly IJSRuntime _jsRuntime;

    public Css(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    public async Task ShowBodyOverflow() {
        await _jsRuntime.InvokeAsync<string>(JSInteropConstants.ShowBodyOverflow);
    }

    public async Task<string> HideBodyOverflow() {
        return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.HideBodyOverflow);
    }
}
