using Contracts;
using MassTransit;
using Newsletter.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration); // Your DI extension method

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("article", async (IPublishEndpoint publishEndpoint) =>
{
    await publishEndpoint.Publish(new ArticleCreated("Hello World"));
});


await app.RunAsync();
