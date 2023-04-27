using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Helpers;
using OfficeRentApp.Repositories.OfficeRepositories;
using OfficeRentApp.Repositories.RentalRepositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OfficeRentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Data")));
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<ImageManipulation>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
