using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;

namespace StudioHair.Infrascruture.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioPorSenhaENome(string nome, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Nome == nome && x.Senha == senha);
        }
    }
}
