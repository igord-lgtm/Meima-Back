using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
options.AddDefaultPolicy(policy =>
policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Pool de conexões único para a aplicação
var connStr = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton(NpgsqlDataSource.Create(connStr!));

var app = builder.Build();
app.UseCors();


app.Run("0.0.0.0:5165");