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
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        /// <summary>
        /// Cria um filme
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /filme
        ///     { 
        ///        "titulo": "Jurassic Park", 
        ///        "diretorId": 1
        ///     }
        /// </remarks>
        /// <param name="filmeInputPostDto">Titulo do filme e Id do diretor</param>
        /// <response code="200">Sucesso ao criar um filme.</response>
        /// <response code="201">Retorna um filme recém criado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO filmeInputPostDto)
        {
            return Ok(await _filmeService.Cria(filmeInputPostDto));
        }

        /// <summary>
        /// Busca todos os filmes
        /// </summary>
        /// <response code="200">Sucesso ao buscar todos os filmes.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet]
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {
            return await _filmeService.BuscaTodos();
        }

        /// <summary>
        /// Busca um filme por Id
        /// </summary>
        /// <param name="id">Id do filme</param>
        /// <response code="200">Sucesso ao buscar um filme.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {
            return Ok(await _filmeService.BuscaPorId(id));
        }

        /// <summary>
        /// Atualiza um filme
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /filme
        ///     {
        ///        "titulo": "Novo Nome", 
        ///        "diretorId": 1
        ///     }
        /// </remarks>
        /// <param name="id">Id do Filme</param>
        /// <param name="filmeInputPutDto">Titulo do filme e Id do diretor</param>
        /// <response code="200">Sucesso ao atualizar um filme.</response>
        /// <response code="201">Retorna um filme recém atualizado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDto)
        {
            return Ok(await _filmeService.Atualiza(id, filmeInputPutDto));
        }

        /// <summary>
        /// Deleta um filme
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <response code="200">Sucesso ao deletar um filme.</response>
        /// <response code="201">Retorna um filme recém deletado.</response>
        /// <response code="400">Erro de validação.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {
            return Ok(await _filmeService.Deleta(id));
        }
    }
}