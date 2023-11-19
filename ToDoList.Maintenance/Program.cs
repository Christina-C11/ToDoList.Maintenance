using Microsoft.EntityFrameworkCore;
using ToDoList.Maintenance.API.Controllers;
using ToDoList.Maintenance.BusinessRules;
using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MaintenanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(MaintenanceController).Assembly)
    .AddControllersAsServices();

builder.Services.AddScoped<IMaintenanceService,MaintenanceService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
