namespace MeimaBack.Models;

public class Usuario(string email, string senha, int pontos)
{
    public int? Id {get; set;}
    public string Email {get; set;} = email;
    public string Senha {get; set;} = senha;
    public int Pontos {get; set;} = pontos;
}