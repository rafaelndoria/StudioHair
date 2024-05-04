using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;

namespace StudioHair.Infrascruture.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarServicoAsync(Servico servico)
        {
            _context.Servicos.Update(servico);
            await _context.SaveChangesAsync();
        }

        public async Task CriarServicoAsync(Servico servico)
        {
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
        }

        public async Task<Servico> GetServicoPorIdAsync(int id)
        {
            return await _context.Servicos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Servico>> GetServicosAsync()
        {
            return await _context.Servicos.ToListAsync();
        }
    }
}
