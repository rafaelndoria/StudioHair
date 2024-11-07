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
        Task CriarArquivoAsync(Arquivo arquivo);
        Task<Arquivo> GetArquivoProduto(int produtoId);
        Task<Arquivo> GetImagemProduto(int imagemId);
        Task DeletarImagem(Arquivo arquivo);
        Task<List<Produto>> GetProdutosCatalogoAsync(int tamanhoPagina, int paginaAtual, string query);
        Task<int> CountProdutosAsync();
    }
}
