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

        public async Task<List<Agendamento>> FiltrarAgendamentosRelatorio(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            IQueryable<Agendamento> query = _context.Agendamentos;

            if (clienteId != 0)
            {
                query = query.Where(v => v.ClienteId == clienteId);
            }

            switch (periodo)
            {
                case "dia":
                    query = query.Where(v => v.Dia.Date == DateTime.Now.Date);
                    break;
                case "intervalo":
                    query = query.Where(v => v.Dia.Date >= inicial.Date && v.Dia.Date <= final.Date);
                    break;
                case "todos":
                default:
                    break;
            }

            return await query
                            .Include(x => x.Cliente)
                                .ThenInclude(x => x.Pessoa)
                            .Where(x => x.Status != Core.Enums.EAgendamento.Pendente)
                            .OrderBy(x => x.Dia)
                            .ToListAsync();
        }

        public async Task<Agendamento> GetAgendamentoPorId(int id)
        {
            return await _context.Agendamentos.Include(x => x.Cliente).ThenInclude(x => x.Pessoa).Include(x => x.AgendamentoServicos).ThenInclude(x => x.Servico).FirstOrDefaultAsync(x => x.Id == id);
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
