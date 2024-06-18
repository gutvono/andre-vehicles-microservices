using AndreVehicles.Data;
using AndreVehicles.EmployeeApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreVehiclesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreVehiclesContext") ?? throw new InvalidOperationException("Connection string 'AndreVehiclesContext' not found.")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
