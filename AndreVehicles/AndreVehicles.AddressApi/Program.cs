using AndreVehicles.AddressApi.Services;
using AndreVehicles.AddressApi.Utils;
using AndreVehicles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreVehiclesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreVehiclesContext") ?? throw new InvalidOperationException("Connection string 'AndreVehiclesContext' not found.")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection(nameof(MongoConfig)));
builder.Services.AddSingleton<IMongoConfig>(sp => sp.GetRequiredService<IOptions<MongoConfig>>().Value);
builder.Services.AddSingleton<AddressService>();

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
