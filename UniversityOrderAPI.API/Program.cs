using System.Linq.Expressions;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Mappers;
using UniversityOrderAPI.Middleware.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Стройка подключения к базе данных
var connectionString = builder.Configuration.GetConnectionString("db");

// инъекция контекста к базе данных для каждого запроса
builder.Services.AddDbContext<UniversityOrderAPIDbContext>(option=>
    option.UseNpgsql(connectionString,b => b.MigrationsAssembly("UniversityOrderAPI.DAL")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new UniversityApiTokenValidationParameters();
    });

builder.Services.AddSingleton(() =>
{
    var config = new TypeAdapterConfig();
    new RegisterMapper().Register(config);
    return config;
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