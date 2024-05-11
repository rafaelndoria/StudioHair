using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task<RelatorioPeriodoVendasViewModel> RelatorioPeriodoVendas(FiltroRelatorioVendasInputModel inputModel);
        Task<RelatorioTicketMedioViewModel> RelatorioTicketMedio(FiltroRelatorioVendasInputModel inputModel);
        Task<FiltrosVendaViewModel> GetFiltrosVenda(int clienteId, string periodo, DateTime inicial, DateTime final);
    }
}
