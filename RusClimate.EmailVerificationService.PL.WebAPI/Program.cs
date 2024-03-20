using Microsoft.EntityFrameworkCore;
using RusClimate.EmailVerificationService.DAL.Postgres.EF;
using RusClimate.EmailVerificationService.BLL.Service.IoC;
using RusClimate.EmailVerificationService.DAL.Postgres.Repository.IoC;
using RusClimate.EmailVerificationService.Common.Settings.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RusClimate.EmailVerificationService.Common.Data.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DAL + BLL Registration
builder.Services.AddPostgresDal();
builder.Services.AddBllModules();

var rawSettings = new EmailVerificationSettings.RawSettings();
builder.Configuration.GetSection("EmailVerificationSettings").Bind(rawSettings);
builder.Services.TryAddSingleton(provider =>
    new EmailVerificationSettings(rawSettings));

// DB connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add cache
builder.Services.AddMemoryCache();

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
