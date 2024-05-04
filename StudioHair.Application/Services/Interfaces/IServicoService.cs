using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IServicoService
    {
        Task CriarServico(CadastroServicoInputModel inputModel);
        Task<IEnumerable<ServicosViewModel>> GetServicos();
        Task<ServicoViewModel> GetServicoConfig(int id);
        Task<AtualizarServicoInputModel> GetServicoParaUpdate(int id);
        Task AtualziarServico(AtualizarServicoInputModel model);
    }
}
