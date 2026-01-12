using SmartCep.Application.UseCases;
using SmartCep.Domain.Interfaces;
using SmartCep.Infrastructure.ExternalServices.ViaCep;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddHttpClient<ICodeProvider, ViaCepProvider>();

builder.Services.AddScoped<SearchCodeUseCase>();

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