using fuelstationAPI.Data;
using fuelstationAPI.Repository;
using fuelstationAPI.Interfaces;
using fuelstationAPI.Services;
var builder = WebApplication.CreateBuilder(args);

// Register Controllers
builder.Services.AddControllers();

// Register Database Connection
builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<PartyRepository>();
builder.Services.AddScoped<PartyService>();
// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
 
// Configure Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
