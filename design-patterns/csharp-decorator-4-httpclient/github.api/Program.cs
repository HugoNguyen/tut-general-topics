using github.api;
using github.api.DelegatingHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<GitHubSettings>()
    .BindConfiguration(GitHubSettings.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();


builder.Services.AddTransient<RetryHandler>();
builder.Services.AddTransient<LoggingHandler>();

// Each invidual HTTP Request will be applied new instance of GitHubAuthenticationHandler
builder.Services.AddTransient<GitHubAuthenticationHandler>();

// Order:
// GitHubService call HttpClient.GetFromJsonAsync
//  First come LogginHandler
//      Then come GitHubAuthenticationHandler, and return
//  Conntinue after GitHubAuthenticationHandler completed
// Return result
builder.Services.AddHttpClient<GitHubService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.github.com");
})
.AddHttpMessageHandler<RetryHandler>()
.AddHttpMessageHandler<LoggingHandler>()
.AddHttpMessageHandler<GitHubAuthenticationHandler>();
    


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.Run();
