using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tienda_API.Aplicacion.Configuraciones;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Aplicacion.Mediadores.Queries;
using Tienda_API.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureServices();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddTransient<ITokenSesion, TokenSesion>();
builder.Services.AddTransient<IGetCategoria, ObtenerCategoria>();
builder.Services.AddTransient<IUsuarioPerfil, ValidarUsuarioPerfil>();
builder.Services.AddTransient<IValidarToken, ValidarToken>();
builder.Services.AddTransient<IGetProducto, ObtenerProducto>();
builder.Services.AddTransient<IGetProductoAdm, ObtenerProductoAdm>();
builder.Services.AddTransient<IGetUsuario, ObtenerUsuario>();
builder.Services.AddTransient<ISaveUsuario, GuardarUsuario>();
builder.Services.AddTransient<IInsertarCategoria, GuardarCategoria>();
builder.Services.AddTransient<IEnviarCorreo, EnviarCorreo>();
builder.Services.AddTransient<IInsertarPedido,  GuardarPedido>();
builder.Services.AddTransient<IProducto, GuardarProducto>();
builder.Services.AddTransient<IEliminarProducto, EliminarProducto>();
builder.Services.AddTransient<IUpdateProducto, ActualizarProducto>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
    // Aquí puedes agregar otros proveedores de registro si lo deseas
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
                      Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
              Name = "Bearer",
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
