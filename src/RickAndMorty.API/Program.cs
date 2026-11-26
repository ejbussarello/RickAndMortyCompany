using Microsoft.EntityFrameworkCore;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Application.Services;
using RickAndMorty.Infrastructure.Data;
using RickAndMorty.Infrastructure.ExternalServices;
using RickAndMorty.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositório + Serviço
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<CharacterImportService>();

// Typed HttpClient
builder.Services.AddHttpClient<IRickAndMortyApiClient, RickAndMortyApiClient>(client =>
{
    client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();