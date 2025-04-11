using StudioHair.Core.Entities;
using System.Linq.Expressions;

namespace StudioHair.Core.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<int> CriarAgendamento(Agendamento agendamento);
        Task AdicionarServicoAgendamento(AgendamentoServicos agendamentoServico);
        Task<IEnumerable<AgendamentoServicos>> GetServicosPorAgendamentoId(int id);
        Task<List<Agendamento>> GetAgendamentosAsync(Expression<Func<Agendamento, bool>>? filter);
        Task<Agendamento> GetAgendamentoPorId(int id);
        Task AtualizarAgendamentoAsync(Agendamento agendamento);
        Task<List<Agendamento>> FiltrarAgendamentosRelatorio(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<IEnumerable<Agendamento>> GetAgendamentoAnalise(int servicoId, int clienteId, string tipoPeriodo, DateTime dataInicial, DateTime dataFinal);
    }
}
