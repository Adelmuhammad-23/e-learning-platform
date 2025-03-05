using e_learning.Core;
using e_learning.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Connect SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(connectionString)
);
#endregion

#region DependencyInjection

builder.Services.AddCoreDependencis();
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
