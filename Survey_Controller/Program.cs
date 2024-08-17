using Microsoft.EntityFrameworkCore;
using Survey_Controller;
using Survey_DataEntry;
using Survey_Repository;
using Survey_Services;

var builder = WebApplication.CreateBuilder(args);
var externalUrl = builder.Configuration["ExternalOrigin"];


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorsConfiguration();
builder.Services.AddDataAccess();
builder.Services.AddAplicationRepositories();
builder.Services.AddApplicationServices();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowExternalOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
