using Dapper;
using MeimaBack.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Pool de conexões único para a aplicação
var connStr = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton(NpgsqlDataSource.Create(connStr!));

var app = builder.Build();
app.UseCors();

app.MapPost("/criarusuario", async (NpgsqlDataSource db, Usuario usuario) =>
{
    var sql = "insert into usuarios (nome_usua, senha_usua) values(@Nome, @Senha)";
    await using var conn = await db.OpenConnectionAsync();

    await conn.ExecuteAsync(sql, new
    {
        usuario.Nome,
        usuario.Senha
    });

    return Results.StatusCode(201);
});

app.Run("http://0.0.0.0:5165");