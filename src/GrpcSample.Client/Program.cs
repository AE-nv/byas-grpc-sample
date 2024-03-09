// See https://aka.ms/new-console-template for more information
using GrpcSample.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging(logging =>
{
    logging.AddSimpleConsole(simple =>
    {
        simple.SingleLine = true;
        simple.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    });
});
builder.Services.AddHostedService<StreamingWorker>();
builder.Services.AddHostedService<TikTakWorker>();

var host = builder.Build();

await host.RunAsync();
