using StudioHair.Core.Entities;
using System.Linq.Expressions;

namespace StudioHair.Core.Interfaces
{
    public interface IUsuarioRepository 
    {
        Task<Usuario> GetUsuarioPorSenhaENome(string nome, string senha);
        Task CriarUsuarioAsync(Usuario usuario);
        Task<IList<Usuario>> GetUsuariosAsync(int page = 0, int pageSize = 0, Expression<Func<Usuario, bool>> predicate = null);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task UpdateAsync(Usuario usuario);
        Task<ConfiguracaoSistema> GetConfigSistemaPorUsuarioIdAsync(int id);
        Task UpdateConfigSistemaAsync(ConfiguracaoSistema configuracaoSistema);
        Task CriarConfigSistemaAsync(ConfiguracaoSistema configuracaoSistema);
    }
}
