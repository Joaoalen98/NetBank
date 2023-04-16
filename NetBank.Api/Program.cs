using Microsoft.EntityFrameworkCore;
using NetBank.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    var cn = builder.Configuration.GetConnectionString("DefaultConnection")!.Replace("DATABASE_NAME", "NetBankDB");
    x.UseSqlServer(cn);
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
