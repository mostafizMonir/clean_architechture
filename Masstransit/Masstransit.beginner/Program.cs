using MassTransit;
using Masstransit.beginner;
using Masstransit.beginner.Models;
using Masstransit.beginner.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration); // Your DI extension method


builder.Services.AddHttpClient<StocksClient>(httpClient => httpClient.BaseAddress = new Uri(builder.Configuration["AlphaVantage:Api"]!));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("stocks/{ticker}", async (string ticker, StocksClient stocksClient) =>
{
    var stockPriceResponse = await stocksClient.GetStockPrice(ticker);
    return Results.Ok(stockPriceResponse);
});

app.MapPost("stocks", async (PurchaseOrderRequest request, IPublishEndpoint publishEndpoint) =>
{
    var order = new Order
    {
         Id = Guid.NewGuid(),
        Symbol = request.Symbol,
        LimitPrice = request.LimitPrice,
        Quantity = request.Quantity
    };

    await publishEndpoint.Publish(new PurchaseOrderSent(order.Id));
});

await app.RunAsync();

