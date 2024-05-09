using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;
using System.Linq.Expressions;

namespace StudioHair.Application.Services.Interfaces
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private ApplicationDbContext _context;

        public AgendamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarServicoAgendamento(AgendamentoServicos agendamentoServico)
        {
            _context.AgendamentoServicos.Add(agendamentoServico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAgendamentoAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Update(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CriarAgendamento(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return agendamento.Id;
        }

        public async Task<Agendamento> GetAgendamentoPorId(int id)
        {
            return await _context.Agendamentos.Include(x => x.Cliente).ThenInclude(x => x.Pessoa).Include(x => x.AgendamentoServicos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Agendamento>> GetAgendamentosAsync(Expression<Func<Agendamento, bool>>? filter)
        {
            IQueryable<Agendamento> query = _context.Agendamentos;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Include(x => x.Cliente).ThenInclude(x => x.Pessoa).ToListAsync();
        }

        public async Task<IEnumerable<AgendamentoServicos>> GetServicosPorAgendamentoId(int id)
        {
            return await _context.AgendamentoServicos.Include(x => x.Servico).Where(x => x.AgendamentoId == id).ToListAsync();
        }
    }
}
