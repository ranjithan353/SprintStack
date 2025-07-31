using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SprintStack.Data;

var builder = WebApplication.CreateBuilder(args);

// --- PostgreSQL connection string ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// --- Add DbContext with PostgreSQL provider ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- Add controllers ---
builder.Services.AddControllers();

// --- Add Swagger for API documentation ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SprintStack API", Version = "v1" });
});

var app = builder.Build();

// --- Middleware ---
app.UseHttpsRedirection();
app.UseAuthorization();

// --- Enable Swagger in development ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();