using System.Collections.Generic;

namespace WebAPIDotNET5.DTOs
{
    public class FilmeListOutputGetAllDTO
    {
        public int PaginaAtual { get; init; }
        public int TotalPaginas { get; init; }
        public int TotalItens { get; init; }


        public List<FilmeOutputGetAllDTO> Itens { get; init; }
    }
}