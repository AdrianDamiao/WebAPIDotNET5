using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Filme>> Post([FromBody] Filme filme)
        {
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);
        }

        [HttpGet]
        public async Task<ActionResult<List<Filme>>> Get()
        {
            return await _context.Filmes.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> Get(long id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            return Ok(filme);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Filme>> Put(long id, [FromBody] Filme filme)
        {
            filme.Id = id;
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);
        }

    }

}