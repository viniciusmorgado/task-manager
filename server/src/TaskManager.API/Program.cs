using Microsoft.EntityFrameworkCore;
using TaskManager.API.Extensions;
using TaskManager.API.Middlewares;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var origins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

if (origins == null || origins.Length == 0)
    throw new InvalidOperationException("No CORS origins configured. Please set CorsSettings:AllowedOrigins in your configuration.");

builder.Services.AddAutoMapper(typeof(TaskProfile).Assembly);
builder.Services.AddDbContext<TaskManagerContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<TaskManagerContext>();
builder.Services.AddTaskManagerServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins(origins);
    });
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        services.GetRequiredService<TaskManagerContext>().Database.Migrate();
    }
    catch (Exception ex)
    {
        services.GetRequiredService<ILogger<Program>>()
                .LogError(ex, "An error occurred while migrating the database.");
    }
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowAll");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
}
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>();
app.Run();
