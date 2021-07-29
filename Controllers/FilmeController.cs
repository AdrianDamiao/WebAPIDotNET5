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
                throw new Exception("Titulo do filme é obrigatório.");
            }
            else if (filmeInputPostDto.DiretorId == 0)
            {
                return NotFound("Id do filme não pode ser 0.");
            }

            var diretorDoFilme = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == filmeInputPostDto.DiretorId);
            var filme = new Filme(filmeInputPostDto.Titulo, diretorDoFilme.Id);
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            var filmeOutputPostDto = new FilmeOutputPostDTO(filme.Id, filme.Titulo);

            return Ok(filmeOutputPostDto);



        }

        [HttpGet]
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {

            var filmes = await _context.Filmes.ToListAsync();
            if (filmes == null)
            {
                throw new Exception("Filmes não encontrados!");
            }
            var filmeOutputGetAllDto = new List<FilmeOutputGetAllDTO>();
            foreach (Filme filme in filmes)
            {
                filmeOutputGetAllDto.Add(new FilmeOutputGetAllDTO(filme.Id, filme.Titulo));
            }
            return filmeOutputGetAllDto;


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {

            if (id == 0)
            {
                throw new Exception("Id do filme não pode ser 0.");
            }

            var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);
            if (filme == null)
            {
                throw new Exception("Filme não encontrado!");
            }
            var filmeOutputGetByIdDto = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.DiretorId, filme.Diretor.Nome);
            return Ok(filmeOutputGetByIdDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDto)
        {
            if (id == 0)
            {
                throw new Exception("Id do filme não pode ser 0.");
            }
            if (filmeInputPutDto.Titulo == "")
            {
                throw new Exception("Título do filme é obrigatório.");
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
                throw new Exception("Id do filme não pode ser 0.");
            }
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                throw new Exception("Filme não encontrado!");
            }
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);

        }
    }
}