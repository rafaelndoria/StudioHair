using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IClienteService
    {
        Task<int> CriarPessoa(CadastroPessoaInputModel inputModel);
        Task CriarCliente(CadastroClienteInputModel inputModel);
        Task DeletarPessoa(int pessoaId);
        Task<IEnumerable<ClientesViewModel>> GetClientes(int page = 1, int pageSize = 5);
        Task<ClienteViewModel> GetClienteConfig(int id);
        Task InativarCliente(int id);
        Task AtivarCliente(int id);
        Task<UpdateClienteInputModel> GetClienteUpdate(int id);
        Task AtualizarCliente(UpdateClienteInputModel inputModel);
    }
}
