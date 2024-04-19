using StudioHair.Core.Entities;

namespace StudioHair.Core.Interfaces
{
    public interface IUsuarioRepository 
    {
        Task<Usuario> GetUsuarioPorSenhaENome(string nome, string senha);
    }
}
