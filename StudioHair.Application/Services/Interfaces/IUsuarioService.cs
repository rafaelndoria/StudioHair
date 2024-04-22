﻿using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

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
    }
}
