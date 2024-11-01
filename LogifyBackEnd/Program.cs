using System.Text;
using LogifyBackEnd.Data;
using LogifyBackEnd.Services;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configure MongoDB Settings
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register MongoDB Client as a Singleton
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString")));

builder.Services.AddScoped<IMongoDatabase>(s =>
    s.GetRequiredService<IMongoClient>().GetDatabase(builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName")));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetSection("Redis")["ConnectionString"];
    if (string.IsNullOrEmpty(configuration))
    {
        throw new ArgumentNullException(nameof(configuration), "Redis connection string is not configured");
    }
    return ConnectionMultiplexer.Connect(configuration);
});


// Register UserService for dependency injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IPointService, PointService>();
builder.Services.AddScoped<ICacheService, CacheService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure this is called before UseAuthorization
app.UseAuthorization();

app.MapControllers(); // Enable attribute routing for controllers

app.Run();