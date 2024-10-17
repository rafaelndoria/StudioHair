namespace StudioHair.Application.Services.Interfaces
{
    public interface IAuthService
    {
        string GerarJwtToken(string email, string nomeUsuario, string papel, int usuarioId);
        string CriptografarSenha(string senha);
    }
}
