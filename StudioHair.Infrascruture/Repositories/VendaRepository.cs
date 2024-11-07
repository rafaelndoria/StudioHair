using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;
using System.Linq.Expressions;

namespace StudioHair.Infrascruture.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ApplicationDbContext _context;

        public VendaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarVendaAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CriarCarrinhoAsync(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();
            return carrinho.Id; 
        }

        public async Task CriarCarrinhoItemAsync(CarrinhoItem carrinhoItem)
        {
            _context.CarrinhoItems.Add(carrinhoItem);
            await _context.SaveChangesAsync();
        }

        public async Task CriarProdutoVendaAsync(ProdutosVenda produto)
        {
            _context.ProdutosVendas.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CriarVendaAsync(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return venda.Id;
        }

        public async Task DeletarItensCarrinhoAsync(int carrinhoId)
        {
            var carrinhoItens = _context.CarrinhoItems.Where(x => x.CarrinhoId == carrinhoId);
            _context.CarrinhoItems.RemoveRange(carrinhoItens);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarProdutoVenda(ProdutosVenda produtoVenda)
        {
            _context.ProdutosVendas.Remove(produtoVenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarVenda(Venda venda)
        {
            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemCarrinhoPorProdutoIdAsync(int produtoId, int carrinhoId)
        {
            var itensCarrinho = await _context.CarrinhoItems.Where(x => x.ProdutoId == produtoId && x.CarrinhoId == carrinhoId).ToListAsync();
            _context.CarrinhoItems.RemoveRange(itensCarrinho);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Venda>> FiltrarVendasAsync(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            IQueryable<Venda> query = _context.Vendas;

            if (clienteId != 0)
            {
                query = query.Where(v => v.ClienteId == clienteId);
            }

            switch (periodo)
            {
                case "dia":
                    query = query.Where(v => v.DataDaVenda.Date == DateTime.Now.Date);
                    break;
                case "intervalo":
                    query = query.Where(v => v.DataDaVenda.Date >= inicial.Date && v.DataDaVenda.Date <= final.Date);
                    break;
                case "todos":
                default:
                    break;
            }

            return await query.Include(x => x.Cliente).ThenInclude(x => x.Pessoa).ToListAsync();
        }

        public async Task<Carrinho> GetCarrinhoPorClienteIdAsync(int clienteId)
        {
            return await _context.Carrinhos.Include(x => x.CarrinhoItems).ThenInclude(x => x.Produto).FirstOrDefaultAsync(x => x.ClienteId == clienteId);
        }

        public async Task<Carrinho> GetCarrinhoPorIdAsync(int carrinhoId)
        {
            return await _context.Carrinhos.Include(x => x.CarrinhoItems).ThenInclude(x => x.Produto).FirstOrDefaultAsync(x => x.Id == carrinhoId);
        }

        public async Task<List<ProdutosVenda>> GetProdutosVendaAsync(int id)
        {
            return await _context.ProdutosVendas.Include(x => x.Produto).Where(x => x.VendaId == id).ToListAsync();
        }

        public async Task<Venda> GetVendaPorId(int id)
        {
            return await _context.Vendas.Include(x => x.ProdutosVendas).ThenInclude(x => x.Produto).Include(x => x.Cliente).ThenInclude(x => x.Pessoa).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Venda>> GetVendasAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        public Task<List<Venda>> GetVendasRelatorioAsync(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            IQueryable<Venda> query = _context.Vendas.Include(x => x.Cliente).ThenInclude(x => x.Pessoa);

            if (clienteId != 0)
            {
                query = query.Where(v => v.ClienteId == clienteId);
            }

            if (periodo.ToLower() == "dia")
            {
                query = query.Where(v => v.DataDaVenda.Date == DateTime.Today.Date);
            }
            else if (periodo.ToLower() == "intervalo")
            {
                query = query.Where(v => v.DataDaVenda >= inicial && v.DataDaVenda <= final);
            }

            return query.ToListAsync();
        }
    }
}
