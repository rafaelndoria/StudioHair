using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IHomeService
    {
        Task<ResumoHomeViewModel> PrepararResumo();
        Task<ResumoVendasViewModel> PrapararResumoVendas();
        Task<ResumoAgendamentosViewModel> PrepararResumoAgendamentos();
        Task<ResumoProdutosViewModel> PrepararResumoProdutos();
        Task<ResumoServicosViewModel> PrepararResumoServicos();
    }
}
