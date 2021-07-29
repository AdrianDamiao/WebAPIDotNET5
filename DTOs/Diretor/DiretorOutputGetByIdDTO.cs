using System;

namespace WebAPIDotNET5.DTOs
{
    public class DiretorOutputGetByIdDTO
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; set; }



        public DiretorOutputGetByIdDTO(long id, string nome, string email)
        {
            if (nome == null)
            { //Programação defensiva, validação que nao pode ser quebrada
                throw new ArgumentNullException("Nome é nulo");
            }
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}