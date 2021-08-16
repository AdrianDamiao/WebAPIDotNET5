using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNET5.DTOs;
using WebAPIDotNET5.Extensions;

namespace WebAPIDotNET5.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;

        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FilmeOutputPostDTO> Cria(FilmeInputPostDTO filmeInputPostDto)
        {
            var diretorDoFilme = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == filmeInputPostDto.DiretorId);
            var filme = new Filme(filmeInputPostDto.Titulo, diretorDoFilme.Id);
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            var filmeOutputPostDto = new FilmeOutputPostDTO(filme.Id, filme.Titulo);

            return filmeOutputPostDto;
        }

        public async Task<FilmeListOutputGetAllDTO> BuscaPorPaginaAsync(int pagina, int limite, CancellationToken cancellationToken)
        {
            var pagedModel = await _context.Filmes
                    .AsNoTracking() //Não observa modificações na entidade, apenas retorna
                    .OrderBy(p => p.Id)
                    .PaginateAsync(pagina, limite, cancellationToken);

            if (pagina > pagedModel.TotalPaginas) //Temporário
            {
                throw new Exception("Essa página não contem registros");
            }
            if (!pagedModel.Itens.Any())
            {
                throw new Exception("Não existem filmes cadastrados!");
            }

            return new FilmeListOutputGetAllDTO
            {
                PaginaAtual = pagedModel.PaginaAtual,
                TotalPaginas = pagedModel.TotalPaginas,
                TotalItens = pagedModel.TotalItens,
                Itens = pagedModel.Itens.Select(filme => new FilmeOutputGetAllDTO(filme.Id, filme.Titulo)).ToList()
            };
        }

        public async Task<FilmeOutputGetByIdDTO> BuscaPorId(long id)
        {
            var filme = await _context.Filmes
                .Include(filme => filme.Diretor)
                .FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                throw new Exception("Filme não encontrado!");
            }
            var filmeOutputGetByIdDto = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.DiretorId, filme.Diretor.Nome);

            return filmeOutputGetByIdDto;
        }

        public async Task<FilmeOutputPutDTO> Atualiza(long id, FilmeInputPutDTO filmeInputPutDto)
        {
            var filme = new Filme(filmeInputPutDto.Titulo, filmeInputPutDto.DiretorId);
            filme.Id = id;
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            var filmeOutputPutDto = new FilmeOutputPutDTO(filme.Id, filme.Titulo);

            return filmeOutputPutDto;
        }

        public async Task<Filme> Deleta(long id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                throw new Exception("Filme não encontrado!");
            }
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return filme;
        }
    }
}