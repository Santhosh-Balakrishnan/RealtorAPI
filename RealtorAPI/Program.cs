using DataAccess;
using DataAccess.DbInitializer;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using RealtorAPI.Utilities;
using System.Text;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("RealtorDb")));
builder.Services.AddScoped(typeof(IDbInitializer), typeof(DbInitializer));
builder.Services.AddMvc(options =>
{
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
builder.Services.AddScoped<CustomExceptionFilter>();
//builder.Services.AddControllers(options => options.Filters.Add<>());
var apiKey = builder.Configuration.GetSection("ApiKey").ToString();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = typeof(Roles).FullName,
        NameClaimType = "name",
        ValidateIssuerSigningKey=true,
        ValidateIssuer=false,
        ValidateAudience=false,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey))
    });
builder.Services.AddCors(options=>
{
    options.AddPolicy(name:"Default",policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseMiddleware<ApplicationExceptionMiddleWare>();
app.UseHttpsRedirection();
SeedDatabase();
app.UseAuthorization();
app.MapControllers();
app.UseCors("Default");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller}/{action}/{Id?}");
});
app.Run();


void SeedDatabase()
{
    using(var scope=app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
