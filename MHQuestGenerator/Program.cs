using Microsoft.EntityFrameworkCore;
using MHQuestGenerator.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var seriLogCondition = config["Serilog"];

System.Diagnostics.Debug.WriteLine(seriLogCondition);
Console.WriteLine(seriLogCondition);

if (string.Equals(seriLogCondition, "console"))
{
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));
}
else
{
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.File("log.log", rollingInterval: RollingInterval.Day));
}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// DB context is registered through dependency injection
builder.Services.AddDbContext<QuestContext>(opt =>
    opt.UseInMemoryDatabase("Quest"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("mhw", configureClient: client =>
{
    client.BaseAddress = new Uri("https://mhw-db.com/");
});

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
