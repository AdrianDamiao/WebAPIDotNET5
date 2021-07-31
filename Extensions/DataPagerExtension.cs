using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNET5.Models;

namespace WebAPIDotNET5.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
        int pagina,
        int limite,
        CancellationToken cancellationToken) //Cancela caso ocorrer um erro
            where TModel : class //TModel deve ser uma classe
        {
            var paged = new PagedModel<TModel>(); //TModel -> Qualquer classe que for passada, generic

            pagina = (pagina < 0) ? 1 : pagina; //Validação de paginas negativas

            paged.PaginaAtual = pagina;
            paged.TamanhoPagina = limite;

            var totalItensCountTask = query.CountAsync(cancellationToken); //Inicia uma query

            var linhaInicial = (pagina - 1) * limite; //Qual registro marca o inicio da página
            paged.Itens = await query //Execução da query
                 .Skip(linhaInicial) //Ignora tudo que vem antes de startRow, ou seja, ignora as paginas anteriores
                 .Take(limite) //Pega o limite, os proximos itens 
                 .ToListAsync(cancellationToken);

            paged.TotalItens = await totalItensCountTask; //Total de registros do banco
            paged.TotalPaginas = (int)Math.Ceiling(paged.TotalItens / (double)limite); //Total de paginas -> Qtd itens / numero de paginas

            return paged;
        }
    }
}