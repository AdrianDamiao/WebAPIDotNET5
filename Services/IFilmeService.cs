using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNET5.DTOs;

public interface IFilmeService
{
    Task<FilmeOutputPostDTO> Cria(FilmeInputPostDTO filmeInputPostDto);
    Task<List<FilmeOutputGetAllDTO>> BuscaTodos();
    Task<FilmeOutputGetByIdDTO> BuscaPorId(long id);
    Task<FilmeOutputPutDTO> Atualiza(long id, FilmeInputPutDTO filmeInputPutDto);
    Task<Filme> Deleta(long id);
}