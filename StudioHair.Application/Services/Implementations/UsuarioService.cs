using Microsoft.EntityFrameworkCore;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
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

        public async Task CriarUsuario(CadastroUsuarioInputModel inputModel)
        {
            var senhaCriptografada = _authService.CriptografarSenha(inputModel.Senha);

            var usuario = new Usuario(inputModel.Nome,
                                      inputModel.Email,
                                      inputModel.Papel,
                                      senhaCriptografada);
            if (await ExisteUsuarioEmailNome(inputModel.Email, inputModel.Nome))
                throw new Exception("Email ou usuário já cadastrado no sistema");
            await _usuarioRepository.CriarUsuarioAsync(usuario);
        }

        public async Task<string> Login(UsuarioLoginInputModel inputModel)
        {
            var senhaCriptografada = _authService.CriptografarSenha(inputModel.Senha);

            var usuario = await _usuarioRepository.GetUsuarioPorSenhaENome(inputModel.Nome, senhaCriptografada);

            if (usuario == null)
                throw new Exception("Usuário ou senha incorretos");

            if (usuario.Ativo == false)
                throw new Exception("Usuário inativo no sistema");

            var token = _authService.GerarJwtToken(usuario.Email, usuario.Nome, usuario.Papel.ToString());

            return token;
        }

        public async Task<bool> ExisteUsuarioEmailNome(string email, string nomeUsuario)
        {
            var usuario = await _usuarioRepository.GetUsuariosAsync(0,0,x => x.Email == email || x.Nome == nomeUsuario);
            if (usuario.Count() == 0)
                return false;

            return true;
        }

        public async Task<IEnumerable<UsuariosViewModel>> GetUsuarios(int page = 1, int pageSize = 5)
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync(page, pageSize);
            var usuariosViewModel = usuarios.Select(x =>
                                                        new UsuariosViewModel(x.Id, x.Nome, x.Email, Enum.GetName(typeof(EPapelUsuario), x.Papel), x.Ativo ? "Ativo" : "Inativo"));

            return usuariosViewModel;
        }

        public async Task<bool> VerificarProximaPagina(int page, int pageSize)
        {
            int startIndex = (page - 1) * pageSize;
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            var total = usuarios.Count();

            return startIndex + pageSize < total;
        }

        public async Task<UsuarioViewModel> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            var usuarioViewModel = new UsuarioViewModel(usuario.Id,
                                                        usuario.Nome,
                                                        usuario.Senha,
                                                        usuario.Email,
                                                        usuario.DataDeCadastro,
                                                        usuario.Ativo,
                                                        Enum.GetName(typeof(EPapelUsuario), usuario.Papel));

            return usuarioViewModel;
        }

        public async Task InativarUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não existe no sistema");
            usuario.InativarUsuario();
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task AtivarUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não existe no sistema");
            usuario.AtivarUsuario();
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task RedefinirSenha(RedefinirSenhaInputModel inputModel)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(inputModel.Id);
            if (usuario == null)
                throw new Exception("Usuário não existe no sistema");
            var senhaCriptografada = _authService.CriptografarSenha(inputModel.Senha);
            usuario.RedefinirSenha(senhaCriptografada);
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<UpdateUsuarioInputModel> GetUsuarioUpdate(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return null;
            var updateUsuarioInputModel = new UpdateUsuarioInputModel()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Nivel = Enum.GetName(typeof(EPapelUsuario), usuario.Papel)
            };

            return updateUsuarioInputModel;
        }

        public async Task UpdateUsuario(UpdateUsuarioInputModel inputModel)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(inputModel.Id);
            if (usuario == null)
                throw new Exception("Usuário não existe no sistema");
            EPapelUsuario nivel = (EPapelUsuario)Enum.Parse(typeof(EPapelUsuario), inputModel.Nivel);
            usuario.AtualizarUsuario(inputModel.Nome, inputModel.Email, nivel);
            await _usuarioRepository.UpdateAsync(usuario);
        }
    }
}
