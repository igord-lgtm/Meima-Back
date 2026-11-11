namespace MeimaBack.Models;

public class Resposta(int id, string descricao, int id_usua, int id_perg)
{
    public int Id {get; set;} = id;
    public string Descricao {get; set;} = descricao;
    public int Id_usua {get; set;} = id_usua;
    public int Id_perg {get; set;} = id_perg;
}