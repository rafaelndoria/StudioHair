using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task<RelatorioPeriodoVendasViewModel> RelatorioPeriodoVendas(FiltroRelatorioVAInputModel inputModel);
        Task<RelatorioTicketMedioViewModel> RelatorioTicketMedio(FiltroRelatorioVAInputModel inputModel);
        Task<RelatorioPeriodoAgendamentosViewModel> RelatorioPeriodoAgendamentos(FiltroRelatorioVAInputModel inputModel);
        Task<FiltrosViewModel> GetFiltros(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<RelatorioFrequenciaSalaoViewModel> RelatorioFrequenciaSalao(FiltroRelatorioVAInputModel inputModel);
    }
}
