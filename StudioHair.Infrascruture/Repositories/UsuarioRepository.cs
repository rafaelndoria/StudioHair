using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;
using System.Linq.Expressions;

namespace StudioHair.Infrascruture.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> GetUsuarioPorSenhaENome(string nome, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Nome == nome && x.Senha == senha);
        }

        public async Task<IList<Usuario>> GetUsuariosAsync(int page = 0, int pageSize = 0, Expression<Func<Usuario, bool>> predicate = null)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (page == 0 && pageSize == 0)
            {
                return await query.ToListAsync();
            }

            int startIndex = (page - 1) * pageSize;

            IList<Usuario> usuarios = await query.Skip(startIndex).Take(pageSize).ToListAsync();

            return usuarios;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
