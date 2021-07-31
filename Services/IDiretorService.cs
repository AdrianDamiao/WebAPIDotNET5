using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNET5.DTOs;

public interface IDiretorService
{
    Task<DiretorOutputPostDTO> Cria(DiretorInputPostDTO diretorInputPostDto);
    Task<List<DiretorOutputGetAllDTO>> BuscaTodos();
    Task<DiretorOutputGetByIdDTO> BuscaPorId(long id);
    Task<DiretorOutputPutDTO> Atualiza(long id, DiretorInputPutDTO diretorInputPutDto);
    Task<Diretor> Deleta(long id);
}