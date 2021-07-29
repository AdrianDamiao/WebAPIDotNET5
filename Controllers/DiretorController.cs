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

        [HttpPost] //POST -> api/diretores
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto) // [FromBody] - Vem do corpo da requisição  
        {
            if (diretorInputPostDto.Nome == "") //TEMPORÁRIO
            {
                return NotFound("O nome do diretor é obrigatório.");
            }
            else if (diretorInputPostDto.Email == "")
            {
                return NotFound("O email do diretor é obrigatório.");
            }

            var diretor = new Diretor(diretorInputPostDto.Nome, diretorInputPostDto.Email);
            await _context.Diretores.AddAsync(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputPostDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome, diretor.Email);

            return Ok(diretorOutputPostDto);
        }

        [HttpGet]
        public async Task<List<Diretor>> Get() //Toda vez que for async tem que ter uma Task
        {
            return await _context.Diretores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Diretor>> Get(long id)
        {
            if (id == 0) //TEMPORÁRIO
            {
                return NotFound("Id do diretor não pode ser 0.");
            }
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            return Ok(diretor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorInputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDto)
        {
            if (id == 0) //TEMPORÁRIO
            {
                return NotFound("Id do diretor não pode ser 0.");
            }
            if (diretorInputPutDto.Nome == "")
            {
                return NotFound("O nome do diretor é obrigatório.");
            }
            else if (diretorInputPutDto.Email == "")
            {
                return NotFound("O email do diretor é obrigatório.");
            }

            var diretor = new Diretor(diretorInputPutDto.Nome, diretorInputPutDto.Email);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome, diretor.Email);

            return Ok(diretorOutputDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);


            if (id == 0) //TEMPORÁRIO
            {
                return NotFound("Id do diretor não pode ser 0.");
            }
            if (diretor == null)
            {
                return NotFound("Diretor não existe.");
            }

            _context.Remove(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }



    }
}
