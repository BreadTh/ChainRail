using Microsoft.AspNetCore.Mvc;
using BreadTh.ChainRail.Example.Server.Requests;
using BreadTh.ChainRail.Example.Server.Persistance;
using BreadTh.ChainRail;
using BreadTh.ChainRail.Example.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Users>();
builder.Services.AddSingleton<ChainRail>();
var app = builder.Build();

app.MapPost("/users", async ([FromBody] UserRequest body, [FromServices] Users users, HttpResponse response) =>
    await users
        .Create(body)
        .WriteToHttpResonse(response)
);

app.MapGet("/users", async ([FromServices] Users users, HttpResponse response) =>
    await users
        .GetAll()
        .WriteToHttpResonse(response)
);

app.Run();



