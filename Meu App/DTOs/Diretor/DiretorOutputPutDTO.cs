namespace WebAPIDotNET5.DTOs
{
    public class DiretorOutputPutDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }



        public DiretorOutputPutDTO(long id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}