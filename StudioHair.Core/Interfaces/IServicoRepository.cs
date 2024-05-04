using StudioHair.Core.Entities;

namespace StudioHair.Core.Interfaces
{
    public interface IServicoRepository
    {
        Task CriarServicoAsync(Servico servico);
        Task<IEnumerable<Servico>> GetServicosAsync();
        Task<Servico> GetServicoPorIdAsync(int id);
        Task AtualizarServicoAsync(Servico servico);
    }
}
