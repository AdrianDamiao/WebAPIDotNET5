using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNET5.DTOs;

namespace WebAPIDotNET5.Controllers
{

    [ApiController] // Diz que a classe Controller é uma API   
    [Route("[controller]")] // Rota do recurso
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco
        public DiretorController(ApplicationDbContext context) // Injeção de dependência -> pra construir um controller é necessario uma classe de contexto
        {
            _context = context;
        }


        /// <summary>
        /// Cria um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /diretores
        ///     {
        ///        
        ///        "nome": "Steven Spielberg", 
        ///        "email": "steven.spielberg@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="diretorInputPostDto">Nome e email do diretor</param>
        /// <response code="200">Sucesso ao criar um diretor.</response>
        /// <response code="201">Retorna um diretor recém criado.</response>
        /// <response code="400">Se a requisição tiver valor null.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPost] //POST -> api/diretores
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto) // [FromBody] - Vem do corpo da requisição  
        {


            if (diretorInputPostDto.Nome == "") //TEMPORÁRIO
            {
                throw new Exception("Nome do diretor é obrigatório.");
            }
            else if (diretorInputPostDto.Email == "")
            {
                throw new Exception("Email do diretor é obrigatório.");
            }

            var diretor = new Diretor(diretorInputPostDto.Nome, diretorInputPostDto.Email);
            await _context.Diretores.AddAsync(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputPostDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome, diretor.Email);

            return Ok(diretorOutputPostDto);



        }

        /// <summary>
        /// Busca todos os diretores
        /// </summary>
        /// <response code="200">Sucesso ao buscar todos os diretores.</response>
        /// <response code="400">Se a requisição tiver valor null.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet]
        public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get() //Toda vez que for async tem que ter uma Task
        {

            var diretores = await _context.Diretores.ToListAsync();
            if (diretores == null)
            {
                throw new Exception("Diretores não encontrados!");
            }
            var diretorOutputGetAllDto = new List<DiretorOutputGetAllDTO>();
            foreach (Diretor diretor in diretores)
            {
                diretorOutputGetAllDto.Add(new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome, diretor.Email));
            }
            return diretorOutputGetAllDto;

        }

        /// <summary>
        /// Busca um diretor pelo Id
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <response code="200">Sucesso ao buscar um diretor.</response>
        /// <response code="400">Se a requisição tiver valor null.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {

            if (id == 0) //TEMPORÁRIO
            {
                throw new Exception("Id do diretor não pode ser 0.");
            }

            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            if (diretor == null)
            {
                throw new Exception("Diretor não encontrado!");
            }
            var diretorOutputGetByIdDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome, diretor.Email);
            return Ok(diretorOutputGetByIdDto);
        }

        /// <summary>
        /// Atualiza um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /diretores
        ///     {
        ///        
        ///        "nome": "Novo Nome", 
        ///        "email": "NovoEmail@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <param name="diretorInputPutDto">Nome e email do diretor</param> 
        /// <response code="200">Sucesso ao atualizar um diretor.</response>
        /// <response code="201">Retorna um diretor recém atualizado.</response>
        /// <response code="400">Se a requisição tiver valor null.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorInputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDto)
        {

            if (id == 0) //TEMPORÁRIO
            {
                throw new Exception("Id do diretor não pode ser 0.");
            }
            if (diretorInputPutDto.Nome == "")
            {
                throw new Exception("Nome do diretor é obrigatório.");
            }
            else if (diretorInputPutDto.Email == "")
            {
                throw new Exception("Email do diretor é obrigatório.");
            }

            var diretor = new Diretor(diretorInputPutDto.Nome, diretorInputPutDto.Email);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome, diretor.Email);

            return Ok(diretorOutputDto);
        }

        /// <summary>
        /// Deleta um diretor
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <response code="200">Sucesso ao deletar um diretor.</response>
        /// <response code="201">Retorna um diretor recém deletado.</response>
        /// <response code="400">Se a requisição tiver valor null.</response> //Adicionar 409 futuramente 
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            if (id == 0) //TEMPORÁRIO
            {
                throw new Exception("Id do diretor não pode ser 0.");
            }
            if (diretor == null)
            {
                throw new Exception("Diretor não encontrado!");
            }

            _context.Remove(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }
    }
}
