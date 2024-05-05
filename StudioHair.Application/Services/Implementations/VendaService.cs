using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;
using System.Globalization;

namespace StudioHair.Application.Services.Implementations
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public VendaService(IVendaRepository vendaRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<decimal> AdicionarProdutoVenda(CadastroVendaInputModel inputModel)
        {
            var valorUnitario = decimal.Parse(inputModel.ValorUnitario.Replace("R$", "").Trim(), CultureInfo.InvariantCulture);
            var produtoVenda = new ProdutosVenda(valorUnitario, inputModel.Quantidade, inputModel.VendaId, inputModel.ProdutoId);
            await _vendaRepository.CriarProdutoVendaAsync(produtoVenda);

            var venda = await _vendaRepository.GetVendaPorId(inputModel.VendaId);
            venda.AdicionarValor(produtoVenda.ValorTotal);

            await _vendaRepository.AtualizarVendaAsync(venda);

            return (decimal)venda.Total;
        }

        public async Task<int> CriarVenda(CadastroVendaInputModel inputModel)
        {
            var venda = new Venda(Enum.Parse<ETipoPagamento>(inputModel.TipoPagamento), inputModel.ClienteId);
            var id = await _vendaRepository.CriarVendaAsync(venda);
            return id;
        }

        public async Task DeletarVenda(int id)
        {
            var venda = await _vendaRepository.GetVendaPorId(id);
            foreach (var produto in venda.ProdutosVendas)
            {
                await _vendaRepository.DeletarProdutoVenda(produto);
            }
            venda.ProdutosVendas = null;
            await _vendaRepository.DeletarVenda(venda);
        }

        public async Task<IEnumerable<VendasViewModel>> FiltrarVendas(FiltroListVendasInputModel inputModel)
        {
            var vendas = await _vendaRepository.FiltrarVendasAsync(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);
            var vendasViewModel = vendas.Select(x =>
                                                    new VendasViewModel(x.Id, x.Cliente.Pessoa.Nome, x.DataDaVenda, (decimal)x.Total, Enum.GetName(typeof(ETipoPagamento), x.TipoPagamento)));
            return vendasViewModel;
        }

        public async Task<DetalhesVendaViewModel> GetDetalhesVenda(int id)
        {
            var venda = await _vendaRepository.GetVendaPorId(id);
            if (venda == null)
                throw new Exception("Venda não encontrada");

            var vendaViewModel = new DetalhesVendaViewModel(venda.Id,
                                                            venda.Cliente.Pessoa.Nome,
                                                            venda.DataDaVenda,
                                                            Enum.GetName(typeof(ETipoPagamento), venda.TipoPagamento),
                                                            (decimal)venda.ValorDesconto,
                                                            (decimal)venda.Total);
            var produtosVendaViewModel = venda.ProdutosVendas.Select(x => 
                                                            new ProdutosVendaViewModel(x.ProdutoId,
                                                                                       x.Produto.Nome,
                                                                                       x.ValorUnitario,
                                                                                       x.Quantidade,
                                                                                       x.ValorTotal));
            vendaViewModel.ProdutosVenda = produtosVendaViewModel;
            return vendaViewModel;
        }

        public async Task<List<ProdutosVendaViewModel>> GetProdutosVenda(int id)
        {
            var produtosVenda = await _vendaRepository.GetProdutosVendaAsync(id);
            var produtosVendaViewModel = produtosVenda.Select(x =>
                                                                   new ProdutosVendaViewModel(x.Id, x.Produto.Nome, x.ValorUnitario, x.Quantidade, x.ValorTotal)).ToList();
            return (List<ProdutosVendaViewModel>)produtosVendaViewModel;
        }

        public async Task<CadastroVendaInputModel> PrepararVendas()
        {
            var clientes = await _clienteRepository.GetClientesAsync(1, 999999999);
            var produtos = await _produtoRepository.GetProdutosAsync();

            if (clientes.Count() <= 0 || produtos.Count() <= 0)
                throw new Exception("Antes de vendes realize o cadastro de alguns produtos ou clientes");

            var clientesViewModel = clientes.Select(x =>
                                                        new ClienteVendaViewModel(x.Id, x.Pessoa.Nome));
            var produtosViewModel = produtos.Select(x =>
                                                        new ProdutoVendaViewModel(x.Id, x.Nome, x.ValorPraticado));
            var cadastroVendaInputModel = new CadastroVendaInputModel()
            {
                Clientes = clientesViewModel,
                Produtos = produtosViewModel
            };
            return cadastroVendaInputModel;
        }

        public async Task SalvarVenda(CadastroVendaInputModel inputModel)
        {
            var venda = await _vendaRepository.GetVendaPorId(inputModel.VendaId);
            if (venda == null)
                throw new Exception("Venda não existe");

            decimal valorDesconto = 0;
            if (inputModel.ValorDesconto != null)
            {
                valorDesconto = decimal.Parse(inputModel.ValorDesconto.Replace("R$", "").Trim(), CultureInfo.InvariantCulture);
                venda.AplicarDesconto(valorDesconto);
            }

            venda.Update(Enum.Parse<ETipoPagamento>(inputModel.TipoPagamento), inputModel.ClienteId, valorDesconto);

            await _vendaRepository.AtualizarVendaAsync(venda);
        }
    }
}
