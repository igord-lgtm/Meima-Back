namespace MeimaBack.Models;

public class Usuario(int id, string nome, string senha, int pontos)
{
    public int Id {get; set;} = id;
    public string Nome {get; set;} = nome;
    public string Senha {get; set;} = senha;
    public int Pontos {get; set;} = pontos;
}