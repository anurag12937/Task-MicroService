using CodingChallenge.Configurations;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodingChallengeAPI", Version = "v1" });

    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("no-cors", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.WithHeaders("Content-Type");
    });
});
//Add Custom services to the container
builder.Services.ServiceCollection(builder.Configuration);

var app = builder.Build();
app.UseCors("no-cors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
[ExcludeFromCodeCoverage]
public partial class Program
{
}
