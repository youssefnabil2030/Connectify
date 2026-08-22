using System.Text;
using connectify.api.middlewares;
using connectify.application.interfaces;
using connectify.application.services;
using connectify.domain.interfaces;
using connectify.infrastructure.hubs;
using connectify.infrastructure.identity;
using connectify.infrastructure.persistence;
using connectify.infrastructure.persistence.repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add Services to DI Container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<connectifydbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection - Domain & Infrastructure Services
builder.Services.AddScoped<iunitofwork, unitofwork>();
builder.Services.AddScoped(typeof(igenericrepository<>), typeof(genericrepository<>));
builder.Services.AddScoped<jwttokengenerator>();

// Dependency Injection - Application Services
builder.Services.AddScoped<iauthservice, authservice>();
builder.Services.AddScoped<icommentservice, commentservice>();
builder.Services.AddScoped<ichatservice, chatservice>();

// SignalR Real-time Services
builder.Services.AddSignalR();

// JWT Authentication Configuration
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "super_secret_key_connectify_2026";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// CORS Policy for Next.js Client
builder.Services.AddCors(options =>
    options.AddPolicy("AllowClient", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()));

var app = builder.Build();

// HTTP Request Pipeline Middleware
app.UseMiddleware<exceptionhandlingmiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SignalR Endpoints Map
app.MapHub<chathub>("/hubs/chat");
app.MapHub<notificationhub>("/hubs/notification");

app.Run();
