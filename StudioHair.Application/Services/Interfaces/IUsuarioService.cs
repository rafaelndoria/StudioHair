using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using System.Security.Claims;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> Login(UsuarioLoginInputModel inputModel);
        Task CriarUsuario(CadastroUsuarioInputModel inputModel);
        Task<bool> ExisteUsuarioEmailNome(string email, string nomeUsuario);
        Task<IEnumerable<UsuariosViewModel>> GetUsuarios(int page = 1, int pageSize = 5);
        Task<bool> VerificarProximaPagina(int page, int pageSize);
        Task<UsuarioViewModel> GetUsuarioById(int id);
        Task InativarUsuario(int id);
        Task AtivarUsuario(int id);
        Task RedefinirSenha(RedefinirSenhaInputModel inputModel);
        Task<UpdateUsuarioInputModel> GetUsuarioUpdate(int id);
        Task UpdateUsuario(UpdateUsuarioInputModel inputModel);
        Task<EPapelUsuario> GetPapelUsuario(UsuarioLoginInputModel inputModel);
        Task<Usuario> GetUsuarioLogado(ClaimsPrincipal tokenUsuario);
        Task<ConfigSistemaInputModel> GetConfigSistemaAsync(int usuarioId);
        Task UpdateConfigSistema(ConfigSistemaInputModel inputModel);
    }
}
