using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;
using System.Linq;
using System;

namespace StudioHair.Infrascruture.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarClienteAsync(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPessoaAsync(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task CriarClienteAsync(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CriarPessoaAsync(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa.Id;
        }

        public async Task DeletarPessoaAsync(int pessoaId)
        {
            var pessoa = await GetPessoaPorId(pessoaId);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente> GetClientePorId(int id)
        {
            return await _context.Cliente.Include(x => x.Pessoa).ThenInclude(x => x.Usuario).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync(int page = 1, int pageSize = 5)
        {
            IQueryable<Cliente> query = _context.Cliente.Include(x => x.Pessoa);

            if (page == 0 && pageSize == 0)
            {
                return await query.ToListAsync();
            }

            int startIndex = (page - 1) * pageSize;

            IEnumerable<Cliente> clientes = await query.Skip(startIndex).Take(pageSize).ToListAsync();

            return clientes;
        }

        public async Task<Pessoa> GetPessoaPorId(int pessoaId)
        {
            return await _context.Pessoa.FirstOrDefaultAsync(x => x.Id == pessoaId);
        }
    }
}
