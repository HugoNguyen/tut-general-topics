using bookstoreapp.blazor.webassembly.ui;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using bookstoreapp.blazor.webassembly.ui.Configurations;
using bookstoreapp.blazor.webassembly.ui.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using bookstoreapp.blazor.webassembly.ui.Services;
using bookstoreapp.blazor.webassembly.ui.Services.Authentication;
using bookstoreapp.blazor.webassembly.ui.Services.Base;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7094") });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
    p.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IClient, Client>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

await builder.Build().RunAsync();
