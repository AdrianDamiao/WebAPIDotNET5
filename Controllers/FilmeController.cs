using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNET5.DTOs;

namespace WebAPIDotNET5.Controllers
{

    [ApiController] // Diz que a classe Controller é uma API  
    [Route("api/[controller]")] // Rota do recurso
    public class FilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO filmeInputPostDto)
        {
            if (filmeInputPostDto.Titulo == "") //TEMPORÁRIO
            {
                return NotFound("O Titulo do filme é obrigatório.");
            }
            else if (filmeInputPostDto.DiretorId == 0)
            {
                return NotFound("O Id do filme não pode ser 0.");
            }

            var diretorDoFilme = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == filmeInputPostDto.DiretorId);
            var filme = new Filme(filmeInputPostDto.Titulo, diretorDoFilme.Id);
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            var filmeOutputPostDto = new FilmeOutputPostDTO(filme.Id, filme.Titulo);

            return Ok(filmeOutputPostDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<Filme>>> Get()
        {
            return await _context.Filmes.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> Get(long id)
        {
            if (id == 0)
            {
                return NotFound("O id do filme não pode ser 0.");
            }
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            return Ok(filme);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDto)
        {
            if (id == 0)
            {
                return NotFound("O id do filme não pode ser 0.");
            }
            if (filmeInputPutDto.Titulo == "")
            {
                return NotFound("O titulo do filme é obrigatório.");
            }

            var filme = new Filme(filmeInputPutDto.Titulo, filmeInputPutDto.DiretorId);
            filme.Id = id;
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            var filmeOutputPutDto = new FilmeOutputPutDTO(filme.Id, filme.Titulo);

            return Ok(filmeOutputPutDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {
            if (id == 0)
            {
                return NotFound("O id do filme não pode ser 0.");
            }
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound("Filme não existe.");
            }
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);
        }

    }

}