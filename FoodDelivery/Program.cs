using Autofac.Extensions.DependencyInjection;
using Autofac;
using Food.Application.Cores.Contexts;
using Food.Infraestructura.Core.Contexts;
using Microsoft.AspNetCore.Identity;
using Food.Core.Securities.Services.Implements;
using Food.Core.Securities.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FoodDelivery.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FoodDelivery.Middlewares;
using Autofac.Core;
using Serilog.Events;
using Serilog;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// utilizando la biblioteca Serilog en una aplicación .NET Core. Serilog es una biblioteca de registro
// altamente configurable que permite redirigir los registros a diferentes destinos,
// como la consola, archivos de registro, bases de datos y servicios de terceros.
var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
    .WriteTo.File(
        ".." + Path.DirectorySeparatorChar + "logapi.log",
        LogEventLevel.Warning,
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Logging.AddSerilog(logger);



// Add services to the container.
builder.Services.AddControllers(options =>
{
    //____Para q reconosca el filtro de validacion
   options.Filters.Add(new ValidationFilter());

    AuthorizationPolicy authorizationPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    
    //______Esto significa que todas las solicitudes a tus endpoints estarán protegidas
    //y requerirán que el usuario esté autenticado y autorizado para acceder a ellos.
    options.Filters.Add(new AuthorizeFilter());
});


//_______________Route Options(modelo maduracion)
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;// urls minuscula
    options.LowercaseQueryStrings = true;

});

//______________ Flu Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//_____________ PasswordHasher
builder.Services.Configure<PasswordHasherOptions>(options =>
{
    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
});

//____________ ISecurityService - registrar la inyeccion
builder.Services.AddTransient<ISecurityService, SecurityService>();


//____________ Config la auth basada en JWT 
string jwtSecretKey = builder.Configuration.GetSection("Security:JwtSecrectKey").Get<string>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>  //add el esquema de auth basado en JWT y configurar sus parámetros.
{
    byte[] key = Encoding.ASCII.GetBytes(jwtSecretKey); //Secret Key se convierte de una cadena en un array de bytes
    options.TokenValidationParameters = new TokenValidationParameters //especifica cómo validar el token.
    {
        IssuerSigningKey = new SymmetricSecurityKey(key), // Key utilizada para firmar y verificar la autenticidad.
        ValidateLifetime = true, //validar la fecha de vencimiento del token.
        ValidIssuer = "",
        ValidAudience = "",
        ValidateIssuer = false,
        ValidateAudience = false,
        //validara que la firma sea válida y coincida con la clave proporcionada en la configuración de la aplicación
        ValidateIssuerSigningKey = true                                                                        
    };
});


//__________ Config Swagger para incluir info sobre la autenticación en la documentación generada
builder.Services.AddSwaggerGen(options =>
{
    //filtro que indica que Endp Requieren Auth
    options.OperationFilter<AuthorizeOperationFilter>(); 
    string schemeName = "Bearer"; 

    options.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme()
    {
        Name = schemeName, //nombre para el esquema de seguridad.
        BearerFormat = "JWT", //El formato del token JWT.
        Scheme = "bearer", //El esquema de autenticación 
        Description = "Add token.", //Una descripción del esquema de seguridad.
        In = ParameterLocation.Header, //ubicación del token en la solicitud (en el encabezado).
        Type = SecuritySchemeType.Http
    });

});



//______________Agregar los servicios de la capa appl y infra
builder.Services.addInfrastructureServices(builder.Configuration); //application
builder.Services.AddApplicationServices(); //infraestructure



//_____________Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(options =>
    {
        options.RegisterModule(new InfraestructureAutofacModule());
        options.RegisterModule(new ApplicationAutofacModule());
    });



//____________ API-registrar Middlewares
builder.Services.AddTransient<ExceptionMiddleware>();



var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Developed-By", "CRISTIAN");
    await next.Invoke();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//______CORS
string[]? corsOrigins = builder.Configuration.GetSection("CorsOrigins:urls").Get<String[]>();

app.UseCors(options =>
{
    options
    .WithOrigins(corsOrigins!)
     .AllowAnyHeader()
    //.AllowAnyOrigin()   permite cualquier origen
    .AllowAnyMethod();    // Permite cualquier método HTTP

});


//______________ Usar Middleware
app.UseMiddleware<ExceptionMiddleware>(); 


app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
