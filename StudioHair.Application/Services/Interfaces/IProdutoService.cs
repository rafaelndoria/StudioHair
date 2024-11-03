using Microsoft.AspNetCore.Http;
using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<int> CriarProduto(CadastroProdutoInputModel inputModel);
        Task<CadastroProdutoInputModel> GetProdutoInputPorId(int id);
        Task CriarProdutoUnidade(CadastroProdutoUnidadeInputModel inputModel);
        Task<List<ProdutoUnidadeViewModel>> GetUnidadesPorProdutoId(int id);
        Task AtualizarProdutoInclusao(CadastroProdutoInputModel inputModel);
        Task DeletarUnidade(int id);
        Task<IEnumerable<ProdutoViewModel>> GetProdutos();
        Task<ProdutoConfigViewModel> GetProdutoConfigPorId(int id);
        Task AtualizarProdutoUnidade(ConfiguracaoUnidadeInputModel inputModel);
        Task AtualizarProduto(AtualizarProdutoInputModel inputModel);
        Task<AtualizarProdutoInputModel> GetProdutoParaAtualizar(int id);
        Task AdicionarImagemProduto(IFormFile arquivo, int produtoId);
        Task<ImagemProdutoViewModel> GetImagemProduto(int produtoId);
        Task DeletarImagemProduto(int imagemId);
    }
}
