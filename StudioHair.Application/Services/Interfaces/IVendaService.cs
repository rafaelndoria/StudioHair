using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IVendaService
    {
        Task<CadastroVendaInputModel> PrepararVendas();
        Task<int> CriarVenda(CadastroVendaInputModel inputModel);
        Task<decimal> AdicionarProdutoVenda(CadastroVendaInputModel inputModel);
        Task<List<ProdutosVendaViewModel>> GetProdutosVenda(int id);
        Task SalvarVenda(CadastroVendaInputModel inputModel);
        Task DeletarVenda(int id);
        Task<IEnumerable<VendasViewModel>> FiltrarVendas(FiltroListVendasInputModel inputModel);
        Task<DetalhesVendaViewModel> GetDetalhesVenda(int id);
    }
}
