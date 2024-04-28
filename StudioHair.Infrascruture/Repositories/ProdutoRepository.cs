using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;

namespace StudioHair.Infrascruture.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade)
        {
            _context.ProdutoUnidades.Update(produtoUnidade);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CriarProdutoAsync(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return produto.Id;
        }

        public async Task CriarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade)
        {
            _context.ProdutoUnidades.Add(produtoUnidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarProdutoUnidadeAsync(ProdutoUnidade produtoUnidade)
        {
            _context.ProdutoUnidades.Remove(produtoUnidade);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> GetProdutoPorIdAsync(int id)
        {
            return await _context.Produto.Include(x => x.ProdutoUnidades).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<ProdutoUnidade> GetProdutoUnidadePorIdAsync(int id)
        {
            return await _context.ProdutoUnidades.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
