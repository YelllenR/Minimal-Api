//Construction of postgres sql connexion string to use in the DbContext
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using minimalApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnexionBuilder = new SqlConnectionStringBuilder
{
    //Passes the connection string from the json file appsetting
    ConnectionString = builder.Configuration.GetConnectionString("postgresSqlDbConnection"),
    UserID = builder.Configuration["UserId"],
    Password = builder.Configuration["Password"]
};

builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(sqlConnexionBuilder.ConnectionString)
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
