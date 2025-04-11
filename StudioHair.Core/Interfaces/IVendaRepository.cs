using StudioHair.Core.Entities;
using System.Linq.Expressions;

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
        Task<List<Venda>> GetVendasRelatorioAsync(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<int> CriarCarrinhoAsync(Carrinho carrinho);
        Task<Carrinho> GetCarrinhoPorClienteIdAsync(int clienteId);
        Task CriarCarrinhoItemAsync(CarrinhoItem carrinhoItem);
        Task<Carrinho> GetCarrinhoPorIdAsync(int carrinhoId);
        Task DeletarItensCarrinhoAsync(int carrinhoId);
        Task DeleteItemCarrinhoPorProdutoIdAsync(int produtoId, int carrinhoId);
        Task<IEnumerable<Venda>> GetVendasAnalise(int produtoId, int clienteId, string tipoPeriodo, DateTime dataInicial, DateTime dataFinal);
    }
}
