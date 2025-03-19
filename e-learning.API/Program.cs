using e_learning.Core;
using e_learning.infrastructure;
using e_learning.infrastructure.Context;
using e_learning.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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

builder.Services.AddCoreDependencis()
                .AddServicesDependencis()
                .AddInfrastructureDependencis()
                .AddServiceRegistrationDependencis(builder.Configuration);

#endregion

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
