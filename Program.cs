using Dapper;
using MeimaBack.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var connStr = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton(NpgsqlDataSource.Create(connStr!));

var app = builder.Build();
app.UseCors();

app.MapPost("/cadastro", async (NpgsqlDataSource db, Usuario usuario) =>
{
    var sql = "insert into usuarios (nome_usua, senha_usua) values(@Email, @Nome, @Senha)";
    await using var conn = await db.OpenConnectionAsync();

    await conn.QueryAsync(sql, new
    {
        usuario.Email,
        usuario.Nome,
        usuario.Senha
    });

    return Results.Created();
});

app.MapPost("/criarpergunta", async (NpgsqlDataSource db, Pergunta pergunta) =>
{
    var sql = "insert into perguntas (titulo_perg, descricao_perg, materia_perg, id_usua) values (@Titulo, @Descricao, @Materia, @Id_usua)";
    await using var conn = await db.OpenConnectionAsync();

    await conn.QueryAsync(sql, new
    {
        pergunta.Titulo,
        pergunta.Descricao,
        pergunta.Materia,
        pergunta.Id_usua
    });

    return Results.Created();
});

app.MapPost("/criarresposta", async (NpgsqlDataSource db, Resposta resposta) =>
{
    var sql = "insert into respostas (descricao_resp, id_usua, id_perg) values (@Descricao, @Id_usua, @Id_perg)";
    await using var conn = await db.OpenConnectionAsync();

    await conn.QueryAsync(sql, new
    {
        resposta.Descricao,
        resposta.Id_usua,
        resposta.Id_perg
    });

    return Results.Created();
});

app.MapGet("/mostrarperguntas", async (NpgsqlDataSource db) =>
{
    var sql = "select titulo_perg, descricao_perg, materia_perg, data_perg, id_usua from perguntas";
    await using var conn = await db.OpenConnectionAsync();
    var perguntas = await conn.QueryAsync(sql);
    return Results.Ok(perguntas);
});

app.MapGet("/mostrarusuarios", async (NpgsqlDataSource db) =>
{
    var sql = "select * from usuarios";
    await using var conn = await db.OpenConnectionAsync();
    var usuarios = await conn.QueryAsync(sql);
    return Results.Ok(usuarios);
});

app.Run("http://0.0.0.0:5165");