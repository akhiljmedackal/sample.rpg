global using sample.rpg.Models;
global using sample.rpg.Services;
global using sample.rpg.Dto;
global using Microsoft.EntityFrameworkCore;
global using sample.rpg.Data;
global using AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterServices,CharacterServices>();
builder.Services.AddScoped<IRepositoryServices,RepositoryServices>();
builder.Services.AddDbContext<DataContext>(options=>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections")));
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
