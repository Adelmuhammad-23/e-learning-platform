using e_learning.Core;
using e_learning.Core.Middlewares;
using e_learning.Data.Entities.Identity;
using e_learning.Data.Helpers;
using e_learning.infrastructure;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Seeder;
using e_learning.Services;
using e_learning.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(option =>
{
    option.AddPolicy("allowCors", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }
    );
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
});


#region Connect SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(connectionString)
);
#endregion

#region Redis 
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

#endregion
#region Serilog
var loggingConfiguration = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.AddSerilog(loggingConfiguration);
#endregion

#region AddCors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
#endregion

#region DependencyInjection

builder.Services.AddCoreDependencis()
                .AddServicesDependencis()
                .AddInfrastructureDependencis()
                .AddServiceRegistrationDependencis(builder.Configuration);
builder.Services.AddHttpClient<PayPalService>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));


#endregion



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Seeder
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<e_learning.Data.Entities.Identity.Role>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}
#endregion
//Configure the HTTP request pipeline.
app.UseStaticFiles();


app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
