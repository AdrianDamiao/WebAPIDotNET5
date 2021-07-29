public class Filme{

    public long Id { get; set; }
    public string Titulo { get; set; }
    public string Ano { get; set; }
    public string Genero { get; set; }

    public long DiretorId { get; set; }

    public Diretor Diretor { get; set; } //Objeto para navegação, não é obrigatório. Somente por questão de facilidade    
    public Filme(string titulo, long diretorId){
        Titulo = titulo;
        DiretorId = diretorId; // Regra 1
    }
}