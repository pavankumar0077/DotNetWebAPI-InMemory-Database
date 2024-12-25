using DotNetWebAPI_InMemoryDatabase.Data;
using DotNetWebAPI_InMemoryDatabase.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBooks, BookServices>();
builder.Services.AddDbContext<BookContext>(options =>
    options.UseInMemoryDatabase("Book"));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Modified to work in both Development and Docker environments
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetWebAPI_InMemoryDatabase API V1");
        c.RoutePrefix = "swagger";
    });
}

// Enable CORS
app.UseCors();

// Add these middleware components in this specific order
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();