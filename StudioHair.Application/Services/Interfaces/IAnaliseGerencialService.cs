using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Enums;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IAnaliseGerencialService
    {
        Task<FiltroAnaliseInputModel> ObterFiltroAnalises(ETipoAnaliseGerencial tipoAnaliseGerencial);
        Task<DadosAnaliseViewModel> BuscarDadosAnalise(FiltroAnaliseInputModel inputModel);
        Task<DadosFaturamentoMensalViewModel> BuscarFaturamentoMensal(FiltroAnaliseInputModel inputModel);
        Task<DadosServicosMaisProcuradosViewModel> BuscarServicosMaisProcurados(FiltroAnaliseInputModel inputModel);
        Task<DadosProdutosMaisVendidosViewModel> BuscarProdutosMaisVendidos(FiltroAnaliseInputModel inputModel);
        Task<DadosTicketMedioViewModel> BuscarTicketMedio(FiltroAnaliseInputModel inputModel);
        Task<DadosHorarioPicoViewModel> BuscarHorarioPico(FiltroAnaliseInputModel inputModel);
        Task<DadosClientesFrequentesViewModel> BuscarClientesFrequentes(FiltroAnaliseInputModel inputModel);
    }
}
