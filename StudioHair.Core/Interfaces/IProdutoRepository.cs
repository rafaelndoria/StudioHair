using StudioHair.Core.Entities;

namespace StudioHair.Core.Interfaces
{
    public interface IProdutoRepository
    {
        Task<int> CriarProdutoAsync(Produto produto);
        Task<Produto> GetProdutoPorIdAsync(int id);
        Task CriarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade);
        Task<ProdutoUnidade> GetProdutoUnidadePorIdAsync(int id);
        Task AtualizarProdutoAsync(Produto produto);
        Task DeletarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade);
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task AtualizarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade);
    }
}
