using StudioHair.Core.Entities;

namespace StudioHair.Core.Interfaces
{
    public interface IVendaRepository
    {
        Task<int> CriarVendaAsync(Venda venda);
        Task CriarProdutoVendaAsync(ProdutosVenda produto);
        Task<Venda> GetVendaPorId(int id);
        Task AtualizarVendaAsync(Venda venda);
        Task<List<ProdutosVenda>> GetProdutosVendaAsync(int id);
        Task DeletarProdutoVenda(ProdutosVenda produtoVenda);
        Task DeletarVenda(Venda venda);
        Task<List<Venda>> FiltrarVendasAsync(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<List<Venda>> GetVendasAsync();
    }
}
