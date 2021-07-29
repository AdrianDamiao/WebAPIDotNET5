
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPIDotNET5.DTOs;

namespace WebAPIDotNET5.Services
{
    public class DiretorService
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

        public async Task<List<DiretorOutputGetAllDTO>> BuscaTodos()
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
