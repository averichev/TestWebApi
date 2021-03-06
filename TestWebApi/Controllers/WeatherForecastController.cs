using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet(Name = "GrpcTest")]
    public async Task GrpcTest()
    {
        // AppContext.SetSwitch(
        //     "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport",
        //     true
        // );
        using var channel = GrpcChannel.ForAddress(
            "http://grpc:7283"
        );
        var client = new Greeter.GreeterClient(channel);
        var reply = await client.SayHelloAsync(
            new HelloRequest {Name = "GreeterClient"});
        Console.WriteLine("Greeting: " + reply.Message);
    }
}