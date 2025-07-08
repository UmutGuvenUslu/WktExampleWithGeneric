using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PointHomeworkWithGeneric.Data;
using PointHomeworkWithGeneric.Services;
using PointHomeworkWithGeneric.Services.Interfaces;
using PointHomeworkWithGeneric.UnitofWork;
using PointHomeworkWithGeneric.UnitofWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddDbContext<MapObjectDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Basarsoft"),
        sqlOptions => sqlOptions.UseNetTopologySuite()
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5174") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<IValidatonService,ValidationService>();
builder.Services.AddScoped<MapObjectService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5174") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost");
app.UseCors("AllowReactApp");
app.UseCors("AllowFrontend");
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
