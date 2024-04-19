using StudioHair.Application.InputModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> Login(UsuarioLoginInputModel inputModel);
    }
}
