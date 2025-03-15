using Application.Profiles;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors();
//este es para hacer la conexion con el front

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the SQLite connection
var connection = new SqliteConnection("Data Source=Proyecto-Web-Api.db");
connection.Open();

// Set journal mode to DELETE using PRAGMA statement
using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE";
    command.ExecuteNonQuery();
}
builder.Services.AddDbContext<ApplicationContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));

// Repository
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<ISysAdminRepository, SysAdminRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registra un repositorio base genérico para las entidades,
// permitiendo que se realicen operaciones comunes de acceso a datos.
builder.Services.AddScoped<IBaseRepository<Owner>, BaseRepository<Owner>>();
builder.Services.AddScoped<IBaseRepository<SysAdmin>, BaseRepository<SysAdmin>>();
builder.Services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();


// Service
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<ISysAdminService, SysAdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();



builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); //AutoMapper

#region Authentication
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Introduzca el token JWT como: Bearer {token}"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CarWashApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});
//configurar las opciones para la clase AutenticacionServiceOptions
builder.Services.Configure<AutenticacionService.AutenticacionServiceOptions>(
    builder.Configuration.GetSection("Authentication"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });



// configuración de autorización basada en roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy => policy.RequireRole(UserRole.Customer.ToString(), UserRole.SysAdmin.ToString()));
    options.AddPolicy("Owner", policy => policy.RequireRole(UserRole.Owner.ToString(), UserRole.SysAdmin.ToString()));
    options.AddPolicy("SysAdmin", policy => policy.RequireRole(UserRole.SysAdmin.ToString()));


});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));

app.UseAuthorization();

app.MapControllers();

app.Run();
