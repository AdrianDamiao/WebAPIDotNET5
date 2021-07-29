namespace WebAPIDotNET5.DTOs
{
    public class FilmeOutputGetByIdDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }

        public long DiretorId { get; set; }

        public string DiretorNome { get; set; }

        public FilmeOutputGetByIdDTO(long id, string titulo, long diretorId, string diretorNome)
        {
            Id = id;
            Titulo = titulo;
            DiretorId = diretorId;
            DiretorNome = diretorNome;
        }

    }
}