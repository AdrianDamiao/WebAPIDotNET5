namespace WebAPIDotNET5.DTOs
{
    public class FilmeOutputGetAllDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }

        public FilmeOutputGetAllDTO(long id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
}