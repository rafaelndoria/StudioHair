using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;

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
        Task<IEnumerable<RVendasPorPeriodoViewModel>> RVendasPorPeriodo(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<IEnumerable<RTicketMedioViewModel>> RTicketMedio(int clienteId, string periodo, DateTime inicial, DateTime final);
    }
}
