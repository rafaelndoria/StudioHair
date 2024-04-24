using StudioHair.Core.Entities;

namespace StudioHair.Core.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> CriarPessoaAsync(Pessoa pessoa);
        Task CriarClienteAsync(Cliente cliente);
        Task DeletarPessoaAsync(int pessoaId);
        Task<Pessoa> GetPessoaPorId(int pessoaId);
        Task<IEnumerable<Cliente>> GetClientesAsync(int page = 1, int pageSize = 5);
        Task<Cliente> GetClientePorId(int id);
        Task AtualizarClienteAsync(Cliente cliente);
        Task AtualizarPessoaAsync(Pessoa pessoa);
    }
}
