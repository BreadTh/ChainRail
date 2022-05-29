using Microsoft.AspNetCore.Mvc;
using BreadTh.ChainRail.Example.Server.Requests;
using BreadTh.ChainRail.Example.Server.Persistance;
using BreadTh.ChainRail;
using BreadTh.ChainRail.Example.Server.Extensions;
using BreadTh.ChainRail.Example.Server.Persistance.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Users>();
builder.Services.AddSingleton<OutcomeFactory>();
var app = builder.Build();

app.MapPost("/users", async ([FromBody] UserRequest body, [FromServices] Users users, HttpResponse response) =>
    await users
        .Create(body)
        .Pipe((Guid userId) => new { userId = userId.ToString() })
        .HandleHttpResponse(response)
);

app.MapGet("/users", async ([FromServices] Users users, HttpResponse response) =>
    await users
        .GetAll()
        .Pipe((List<User> users) => new { users = users })
        .HandleHttpResponse(response)
);

app.Run();



