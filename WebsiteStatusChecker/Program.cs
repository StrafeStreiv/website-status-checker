using Microsoft.EntityFrameworkCore;
using WebsiteStatusChecker.Data;
using WebsiteStatusChecker.Models;
using WebsiteStatusChecker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Регистрируем HttpClient (он будет жить всё время работы приложения)
builder.Services.AddHttpClient();

// Регистрируем наш фоновый сервис
builder.Services.AddHostedService<UptimeCheckBackgroundService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
