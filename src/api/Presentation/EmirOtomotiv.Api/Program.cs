using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using EmirOtomotiv.Persistence.Contexts;
using EmirOtomotiv.Core.Application;
using EmirOtomotiv.Persistence;
using Serilog;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;
using System.Net.Mime;
using EmirOtomotiv.Presentation.Api.Middlewares;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using EmirOtomotiv.Infrastructure.Authentication;
using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Infrastructure;
using EmirOtomotiv.Presentation.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Cookies["accessToken"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
    
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

// Allow larger multipart bodies for image uploads (up to 50 MB)
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(o =>
{
    o.MultipartBodyLengthLimit = 50 * 1024 * 1024;
});

var connectionString = builder.Configuration.GetConnectionString("EmirOtomotivDb") 
    ?? "Host=localhost;Database=EmirOtomotivDb;Username=postgres;Password=postgres";
builder.Services.AddDbContext<EmirOtomotivDbContext>(options =>
    options.UseNpgsql(connectionString));

Logger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/emirotomotiv.log", rollingInterval: RollingInterval.Day, shared: true)
                //.WriteTo.Seq(builder.Configuration["Seq:ServerURL"] ?? string.Empty)
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddHttpLogging(logging =>
                                {
                                    logging.LoggingFields = HttpLoggingFields.All;
                                    logging.RequestHeaders.Add("sec-ch-ua");
                                    logging.MediaTypeOptions.AddText(MediaTypeNames.Application.Json);
                                    logging.RequestBodyLogLimit  = 4096;
                                    logging.ResponseBodyLogLimit = 4096;
                                });

var allowedOrigins = builder.Configuration["CorsOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries)
    ?? new[] { "http://localhost:3000" };
builder.Services.AddCors(x => x.AddDefaultPolicy(y => y.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(allowedOrigins)));

var app = builder.Build();

// Initialize the database and ensure the latest migrations/data are applied
try
{
    using(var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        IPasswordHasher passwordHashService = services.GetRequiredService<IPasswordHasher>();

        DbInitializer.Initialize(app.Services, passwordHashService);
    }
}
catch (Exception ex)
{
    var log = app.Services.GetRequiredService<ILogger<Program>>();
    log.LogError(ex, "An error occurred while initializing the database.");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseSerilogRequestLogging();
app.UseHttpLogging();
if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
