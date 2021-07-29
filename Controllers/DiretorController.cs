using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDotNET5
{

    [ApiController] // Diz que a classe Controller é uma API
    [Route("[controller]")] // Rota do recurso
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context; //Contexto intermediario para comunicação com o banco
        public DiretorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Diretor>> Post([FromBody] Diretor diretor) // Vem do corpo da requisição 
        {   //Toda vez que for async tem que ter uma Task
            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();
            return Ok(diretor);
        }

        [HttpGet]
        public async Task<List<Diretor>> Get()
        {
            return await _context.Diretores.ToListAsync();
        }

    }
}


