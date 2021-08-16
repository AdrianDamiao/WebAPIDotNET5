using System.Collections.Generic;

namespace WebAPIDotNET5.Models
{
    public class PagedModel<TModel>
    {
        const int TamanhoMaxPagina = 10;
        private int _tamanhoPagina;
        public int TamanhoPagina
        {
            get => _tamanhoPagina;
            set => _tamanhoPagina = (value > TamanhoMaxPagina) ? value : TamanhoMaxPagina;
        }

        public int PaginaAtual { get; set; }
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public IList<TModel> Itens { get; set; }

        public PagedModel()
        {
            Itens = new List<TModel>();
        }
    }
}