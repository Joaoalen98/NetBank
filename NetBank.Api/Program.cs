using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;
using NetBank.Infra.Repos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    var cn = builder.Configuration.GetConnectionString("DefaultConnection")!.Replace("DATABASE_NAME", "NetBankDB");
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
