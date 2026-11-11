namespace MeimaBack.Models;

public class Pergunta(string titulo, string descricao, string materia, string data, int id_usua)
{
    public int? Id {get; set;}
    public string Titulo {get; set;} = titulo;
    public string Descricao {get; set;} = descricao;
    public string Materia {get; set;} = materia;
    public string Data {get; set;} = data;
    public int Id_usua {get; set;} = id_usua;
}