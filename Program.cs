using Microsoft.EntityFrameworkCore;
using StageTest.Application.Services;
using StageTest.Infrastructure;
using StageTest.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StageTest.Infrastructure.Repositories.SubProcessos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.MaxDepth = 64; // Limite de profundidade
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<ProcessoService>();
builder.Services.AddScoped<ResponsavelService>();
builder.Services.AddScoped<EquipeService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProcessosRepository,ProcessosRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IResponsavelRepository, ResponsavelRepository>();
builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
builder.Services.AddScoped<ISubProcessoRepository, SubProcessoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDeveloperExceptionPage();

app.Run();
