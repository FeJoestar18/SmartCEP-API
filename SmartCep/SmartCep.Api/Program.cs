using Microsoft.EntityFrameworkCore;
using SmartCep.Application.UseCases;
using SmartCep.Domain.Interfaces;
using SmartCep.Infrastructure.Persistence;
using SmartCep.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

// Repositories
builder.Services.AddScoped<ICodeRepository, CodeRepository>();

// UseCases
builder.Services.AddScoped<SearchCodeFromDatabaseUseCase>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();