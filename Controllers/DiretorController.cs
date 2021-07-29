using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult<Diretor>> Post([FromBody] Diretor diretor) // Vem do corpo da requisição 
        {   //Toda vez que for async tem que ter uma Task
            await _context.Diretores.AddAsync(diretor);
            await _context.SaveChangesAsync();
            return Ok(diretor);
        }

        [HttpGet]
        public async Task<List<Diretor>> Get()
        {
            return await _context.Diretores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Diretor>> Get(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            return Ok(diretor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Diretor>> Put(long id, [FromBody] Diretor diretor)
        {

            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {

            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            _context.Remove(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }



    }
}
