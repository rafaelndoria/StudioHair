using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Core.Interfaces;

namespace StudioHair.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IAuthService authService, IUsuarioRepository usuarioRepository)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> Login(UsuarioLoginInputModel inputModel)
        {
            var senhaCriptografada = _authService.CriptografarSenha(inputModel.Senha);

            var usuario = await _usuarioRepository.GetUsuarioPorSenhaENome(inputModel.Nome, senhaCriptografada);

            if (usuario == null)
                return null;

            var token = _authService.GerarJwtToken(usuario.Email, usuario.Nome, usuario.Papel.ToString());

            return token;
        }
    }
}
