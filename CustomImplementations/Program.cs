using CustomImplementations.CustomCache;
using CustomImplementations.CustomMiddlewares;
using CustomImplementations.Models;
using CustomImplementations.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection("RateLimitOptions"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<LoggerService>();
builder.Services.AddSingleton<IRateLimiterService, RateLimiterService>();
builder.Services.AddSingleton<ICacheService, InMemoryCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RateLimitMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
