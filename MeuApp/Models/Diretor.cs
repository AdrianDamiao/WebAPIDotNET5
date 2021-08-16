using System.Collections.Generic;

public class Diretor
{

    public long Id { get; set; }

    public string Nome { get; set; }
    public string Email { get; set; }

    public ICollection<Filme> Filmes { get; set; }

    public Diretor(string nome, string email)
    {
        Nome = nome;
        Email = email;
        Filmes = new List<Filme>(); //Construido para poder inicializar uma lista de filmes
    }

}