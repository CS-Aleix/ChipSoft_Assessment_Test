using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient();
builder.Services.ConfigureHttpClientDefaults(o => {
    o.ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
});

await builder.Build().RunAsync();
