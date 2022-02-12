using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Radzen;

using TestJob01.UI_BL;
using TestJob01.UI_BL.Configuration;
using TestJob01.UI_BL.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<TestJob01.UI_BL_Radzen.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
if (null != configSection) {
    builder.Services.Configure<BaseUrlConfiguration>(configSection);
    var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();
    builder.Services.AddScoped(sp => new HttpClient() {
        BaseAddress = new Uri(baseUrlConfig?.ApiBase ?? "")
    });
    builder.Services.AddScoped<HttpService>();
}

builder.Services.AddBlazorServices();
// Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Logging.AddConfiguration(builder.Configuration.GetRequiredSection("Logging"));


await builder.Build().RunAsync();
