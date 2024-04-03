using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WhiskerHaven;
using WhiskerHaven.Application;
using WhiskerHaven.Application.Services.JwtService;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure;
using WhiskerHaven.Infrastructure.Data;
using WhiskerHaven.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)

    //.Enrich.FromLogContext()
    //.WriteTo.File("logs\\log.txt")
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IJwtService), typeof(JwtService));
builder.Services.AddMediatR();
//obtiene todos los ensamblados cargados en el dominio de la aplicación actual. 
//Esto significa que AutoMapper buscará en todos estos ensamblados cualquier perfil de mapeo que hayas definido
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = signinKey
        };
    });

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description =
                "Autenticación JWT para el esquema Bearer. \r\n\r\n " +
                "Ingresa la palabra 'Bearer'seguida de un [espacio] y despues su token en el campo de abajo \r\n\r\n" +
                "Ejemplo: \"Bearer tdashjoadnkla\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name= "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });


/*Soporte para CORS
 *Se pueden habilitar: 1-Un dominio, 2-multiples dominios,
 *3-cualquier dominio (Tener en cuenta seguridad)
 *Usamos de ejemplo el dominio: http://localhost:3223, se debe cambiar por el correcto
  Se usa (*) para todos los dominios
*/
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//Importante para habilitar que se  exponga el directorio de imagenes
//Sin esto no se puede acceder
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ProductImages")),
    RequestPath = new PathString("/ProductImages")
});

//Soporte para Cors
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
