using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using PollyWithHttpClientFactory.Configuration;
using PollyWithHttpClientFactory.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)));
builder.Services.AddSingleton<IApiConfig>(x => x.GetRequiredService<IOptions<ApiConfig>>().Value);

//Criar uma politica de Retry com Polly
AsyncRetryPolicy<HttpResponseMessage> retryPolicy = Policy.HandleResult<HttpResponseMessage>(
        res => !res.IsSuccessStatusCode)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));
       

//var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
//    .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

//Registrar httpClient
builder.Services.AddHttpClient<ITodoService, TodoService>(b =>
    b.BaseAddress = new Uri(builder.Configuration["ApiConfig:BaseUrl"]))
    .AddPolicyHandler(retryPolicy);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
