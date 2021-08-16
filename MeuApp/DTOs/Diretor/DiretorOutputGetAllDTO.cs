namespace WebAPIDotNET5.DTOs
{
    public class DiretorOutputGetAllDTO
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; set; }



        public DiretorOutputGetAllDTO(long id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}