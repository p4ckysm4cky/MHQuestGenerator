using Microsoft.EntityFrameworkCore;
using MHQuestGenerator.Models;

var builder = WebApplication.CreateBuilder(args);

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
