using ConstructionEquipmentRental.Services;
using ConstructionEquipmentRental.Services.Options;
using System.Text.Json.Serialization;
using ConstructionEquipmentRental.Persistent.InMemory;
using ConstructionEquipmentRental.Services.Models;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

services.AddControllers()
    .AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<List<EquipmentListOptions>>(builder.Configuration.GetSection("EquipmentList"));
services.Configure<FeeOptionsEur>(builder.Configuration.GetSection("RentalFeesEur"));
services.AddMediatR(typeof(Program), typeof(RentalEquipmentItem));
services.AddSingleton<IRentalEquipmentInventory, RentalEquipmentInventory>();
services.AddSingleton<IPersistRental, PersistRental>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
