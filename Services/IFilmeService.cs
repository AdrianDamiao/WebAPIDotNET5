using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPIDotNET5.DTOs;

public interface IFilmeService
{
    Task<FilmeOutputPostDTO> Cria(FilmeInputPostDTO filmeInputPostDto);
    Task<FilmeListOutputGetAllDTO> BuscaPorPaginaAsync(int pagina, int limite, CancellationToken cancellationToken);
    Task<FilmeOutputGetByIdDTO> BuscaPorId(long id);
    Task<FilmeOutputPutDTO> Atualiza(long id, FilmeInputPutDTO filmeInputPutDto);
    Task<Filme> Deleta(long id);
}