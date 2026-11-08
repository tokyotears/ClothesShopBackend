using Api.Models;
using Api.Data;
using Api.Repos;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<ClothesCategory>());
});
builder.Services.AddScoped<ClothesRepo>();
builder.Services.AddCors(options => 
{
    options.AddPolicy("Policy", policy => 
    {
    	policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

Directory.CreateDirectory("/app/uploads/images");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider("/app/uploads/images"),
    RequestPath = "/images"
});

app.MapControllers();
app.UseCors("Policy");

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

app.Run();
