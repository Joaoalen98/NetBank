using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;
using NetBank.Infra.Repos;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    var cn = builder.Configuration.GetConnectionString("NetBank") 
    ?? throw new ApplicationException("É necessário definir a string de conexão na variável de ambiente ConnectionStrings:NetBank");
    
    x.UseSqlServer(cn);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        var key = Environment.GetEnvironmentVariable("JwtKey");
        var bytesKey = Encoding.ASCII.GetBytes(key);

        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(bytesKey),
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuer = false,
        };
    });

builder.Services.AddScoped<IUsuarioRepo, UsuarioRepo>();
builder.Services.AddScoped<IContaRepo, ContaRepo>();
builder.Services.AddScoped<ITransacaoRepo, TransacaoRepo>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(x =>
{
    x.AddPolicy("cors", x =>
    {
        x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
