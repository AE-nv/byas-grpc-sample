using GrpcSample.Server.Services;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddLogging(logging =>
{
    logging.AddSimpleConsole(simple =>
    {
        simple.SingleLine = true;
        simple.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CounterService>();
app.MapGrpcService<TikTakService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
