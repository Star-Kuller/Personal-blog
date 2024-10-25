using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalBlog.Api.Extensions;
using PersonalBlog.Api.Services;
using PersonalBlog.Core.Handlers.auth;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Core.PipelineBehavior;
using PersonalBlog.Core.Security;
using PersonalBlog.Infrastructure.Database;
using PersonalBlog.Infrastructure.Database.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors();

builder.Services.AddOptions();

//Add Database
builder.Services.AddDbContext<PbDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<DbInitializer.AdminUser>(builder.Configuration.GetSection("AdminUser"));
builder.Services.AddTransient<IPbDbContext, PbDbContext>();

//Add gRPC
builder.Services.AddGrpc().AddJsonTranscoding();

//Add MediatR and pipeline
builder.Services.AddMediatR(cfg
    => cfg.RegisterServicesFromAssembly(typeof(Login).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Login).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//Add authorization and authentication
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddTransient<ITokenProvider, TokenProvider>();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireClaim(ClaimTypes.Name);
        policy.RequireClaim(ClaimTypes.Role);
    });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = true,
                IssuerSigningKey = tokenOptions?.SecurityKey
            };
    });
builder.Services.AddCurrentUser();

//Add swagger
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
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

//Configure API
var app = builder.Build();

app.UseRouting();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("grpc-status", "grpc-message", "custom-status", "custom-message"));

app.UseSwagger();
if (app.Environment.IsDevelopment())
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });

app.UseAuthentication();
app.UseAuthorization();
app.UseCurrentUser();

// Configure the HTTP request pipeline.
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.MapGrpcService<GreeterService>().EnableGrpcWeb();
app.MapGrpcService<AuthService>().EnableGrpcWeb();
app.MapGrpcService<ArticlesService>().EnableGrpcWeb();

app.UseStaticFiles();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//Initialization
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await DbInitializer.InitializeAsync(services);
}
catch (Exception e)
{
    logger.LogError(e.Message);
    logger.LogError("Database initialization error.");
    throw;
}

app.Run();