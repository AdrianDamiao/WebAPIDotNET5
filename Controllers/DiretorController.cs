using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNET5.DTOs;
using WebAPIDotNET5.Services;

namespace WebAPIDotNET5.Controllers
{

    [ApiController] // Diz que a classe Controller é uma API   
    [Route("[controller]")] // Rota do recurso
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DiretorService _diretorService; //Contexto intermediario para comunicação com o banco
        public DiretorController(ApplicationDbContext context,
                                 DiretorService diretorService) // Injeção de dependência -> pra construir um controller é necessario uma classe de contexto
        {
            _context = context;
            _diretorService = diretorService;
        }

        /// <summary>
        /// Cria um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /diretor
        ///     {
        ///        "nome": "Steven Spielberg", 
        ///        "email": "steven.spielberg@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="diretorInputPostDto">Nome e email do diretor</param>
        /// <response code="200">Sucesso ao criar um diretor.</response>
        /// <response code="201">Retorna um diretor recém criado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPost] //POST -> api/diretores
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto) // [FromBody] - Vem do corpo da requisição  
        {
            return Ok(await _diretorService.Cria(diretorInputPostDto));
        }

        /// <summary>
        /// Busca todos os diretores
        /// </summary>
        /// <response code="200">Sucesso ao buscar todos os diretores.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet]
        public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get() //Toda vez que for async tem que ter uma Task
        {
            return await _diretorService.BuscaTodos();
        }

        /// <summary>
        /// Busca um diretor pelo Id
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <response code="200">Sucesso ao buscar um diretor.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {
            return Ok(await _diretorService.BuscaPorId(id));
        }

        /// <summary>
        /// Atualiza um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /diretor
        ///     {
        ///        "nome": "Novo Nome", 
        ///        "email": "NovoEmail@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <param name="diretorInputPutDto">Nome e email do diretor</param> 
        /// <response code="200">Sucesso ao atualizar um diretor.</response>
        /// <response code="201">Retorna um diretor recém atualizado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDto)
        {
            return Ok(await _diretorService.Atualiza(id, diretorInputPutDto));
        }

        /// <summary>
        /// Deleta um diretor
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <response code="200">Sucesso ao deletar um diretor.</response>
        /// <response code="201">Retorna um diretor recém deletado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            return Ok(await _diretorService.Deleta(id));
        }
    }
}
