
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Linq;
using WebAPIDotNET5.DTOs;
using WebAPIDotNET5.Extensions;

namespace WebAPIDotNET5.Services
{
    public class DiretorService : IDiretorService //Interface -> Boas praticas
    {
        private readonly ApplicationDbContext _context;
        public DiretorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DiretorOutputPostDTO> Cria(DiretorInputPostDTO diretorInputPostDto)
        {
            var diretor = new Diretor(diretorInputPostDto.Nome, diretorInputPostDto.Email);
            await _context.Diretores.AddAsync(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputPostDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome, diretor.Email);

            return diretorOutputPostDto;
        }

        public async Task<DiretorListOutputGetAllDTO> BuscaPorPaginaAsync(int pagina, int limite, CancellationToken cancellationToken)
        {
            var pagedModel = await _context.Diretores
                    .AsNoTracking() //Não observa modificações na entidade, apenas retorna
                    .OrderBy(p => p.Id)
                    .PaginateAsync(pagina, limite, cancellationToken);

            if (pagina > pagedModel.TotalPaginas) //Temporário
            {
                throw new Exception("Essa página não contem registros");
            }
            if (!pagedModel.Itens.Any())
            {
                throw new Exception("Não existem diretores cadastrados!");
            }

            return new DiretorListOutputGetAllDTO
            {
                PaginaAtual = pagedModel.PaginaAtual,
                TotalPaginas = pagedModel.TotalPaginas,
                TotalItens = pagedModel.TotalItens,
                Itens = pagedModel.Itens.Select(diretor => new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome, diretor.Email)).ToList()
            };
        }

        public async Task<DiretorOutputGetByIdDTO> BuscaPorId(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            if (diretor == null)
            {
                throw new Exception("Diretor não encontrado!");
            }
            var diretorOutputGetByIdDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome, diretor.Email);

            return diretorOutputGetByIdDto;
        }

        public async Task<DiretorOutputPutDTO> Atualiza(long id, DiretorInputPutDTO diretorInputPutDto)
        {
            var diretor = new Diretor(diretorInputPutDto.Nome, diretorInputPutDto.Email);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome, diretor.Email);

            return diretorOutputDto;
        }

        public async Task<Diretor> Deleta(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            if (diretor == null)
            {
                throw new Exception("Diretor não encontrado!");
            }

            _context.Remove(diretor);
            await _context.SaveChangesAsync();

            return diretor;
        }
    }
}
