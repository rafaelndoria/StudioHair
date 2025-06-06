﻿using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace StudioHair.Application.Services.Implementations
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;

        public VendaService(IVendaRepository vendaRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository, IAgendamentoRepository agendamentoRepository)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<Carrinho> AdicionarItemCarrinho(AdicionarAoCarrinhoInputModel inputModel, int carrinhoId)
        {
            var carrinhoItem = new CarrinhoItem(inputModel.Quantidade, inputModel.ValorProduto, inputModel.ProdutoId, carrinhoId);
            await _vendaRepository.CriarCarrinhoItemAsync(carrinhoItem);
            var carrinho = await _vendaRepository.GetCarrinhoPorIdAsync(carrinhoId);
            return carrinho;
        }

        public async Task<decimal> AdicionarProdutoVenda(CadastroVendaInputModel inputModel)
        {
            var culture = new CultureInfo("pt-BR");
            var valorUnitario = decimal.Parse(inputModel.ValorUnitario.Replace("R$", "").Trim(), NumberStyles.Currency, culture);

            var produtoVenda = new ProdutosVenda(valorUnitario, inputModel.Quantidade, inputModel.VendaId, inputModel.ProdutoId);
            await _vendaRepository.CriarProdutoVendaAsync(produtoVenda);

            var venda = await _vendaRepository.GetVendaPorId(inputModel.VendaId);
            venda.AdicionarValor(produtoVenda.ValorTotal);

            await _vendaRepository.AtualizarVendaAsync(venda);

            return (decimal)venda.Total;
        }

        public async Task<int> CriarCarrinho(int clienteId)
        {
            var carrinho = new Carrinho(clienteId);
            var id = await _vendaRepository.CriarCarrinhoAsync(carrinho);
            return id;
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

        public async Task EsvaziarItensCarrinho(int carrinhoId)
        {
            await _vendaRepository.DeletarItensCarrinhoAsync(carrinhoId);
        }

        public async Task ExcluirProdutoCarrinho(int produtoId, int carrinhoId)
        {
            await _vendaRepository.DeleteItemCarrinhoPorProdutoIdAsync(produtoId, carrinhoId);
        }

        public async Task<IEnumerable<VendasViewModel>> FiltrarVendas(FiltroListVendasInputModel inputModel)
        {
            var vendas = await _vendaRepository.FiltrarVendasAsync(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);
            var vendasViewModel = vendas.Select(x =>
                                                    new VendasViewModel(x.Id, x.Cliente.Pessoa.Nome, x.DataDaVenda, (decimal)x.Total, Enum.GetName(typeof(ETipoPagamento), x.TipoPagamento)));
            return vendasViewModel;
        }

        public async Task FinalizarCarrinho(int carrinhoId, int clienteId)
        {
            var carrinho = await _vendaRepository.GetCarrinhoPorIdAsync(carrinhoId);
            if (carrinho == null)
                throw new Exception("Não foi possivel encontrar o carrinho");
            if (carrinho.CarrinhoItems.Count == 0)
                throw new Exception("Carrinho não tem itens");

            var venda = new Venda(ETipoPagamento.Dinheiro, clienteId);
            var vendaId = await _vendaRepository.CriarVendaAsync(venda);

            foreach (var item in carrinho.CarrinhoItems)
            {
                var vendaItem = new ProdutosVenda(item.Valor, item.Quantidade, vendaId, item.ProdutoId);
                venda.AdicionarValor(item.Valor * item.Quantidade);
                await _vendaRepository.CriarProdutoVendaAsync(vendaItem);
            }

            await _vendaRepository.AtualizarVendaAsync(venda);
            await EsvaziarItensCarrinho(carrinhoId);
        } 

        public async Task<CarrinhoDetalhesViewModel> GetCarrinhoDetalhes(int carrinhoId)
        {
            var carrinho = await _vendaRepository.GetCarrinhoPorIdAsync(carrinhoId);
            if (carrinho == null)
                throw new Exception("Não foi possivel buscar o carrinho");

            var totalCarrinho = 0m;
            var itensCarrinhoViewModel = new List<ItemCarrinhoViewModel>();

            var itensAgrupados = carrinho.CarrinhoItems
                .GroupBy(item => item.ProdutoId)
                .Select(grupo => new
                {
                    ProdutoId = grupo.Key,
                    Nome = grupo.First().Produto.Nome,
                    QuantidadeTotal = grupo.Sum(item => item.Quantidade),
                    ValorUnitario = grupo.First().Valor
                });

            foreach (var item in itensAgrupados)
            {
                var itemCarrinho = new ItemCarrinhoViewModel(item.Nome, item.QuantidadeTotal, (item.QuantidadeTotal * item.ValorUnitario), item.ValorUnitario, item.ProdutoId);
                itensCarrinhoViewModel.Add(itemCarrinho);
                totalCarrinho += item.QuantidadeTotal * item.ValorUnitario;
            }

            var carrinhoViewModel = new CarrinhoDetalhesViewModel(totalCarrinho);
            carrinhoViewModel.Itens = itensCarrinhoViewModel;

            return carrinhoViewModel;
        }

        public async Task<Carrinho> GetCarrinhoPorClienteId(int clienteId)
        {
            return await _vendaRepository.GetCarrinhoPorClienteIdAsync(clienteId);
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

            List<Produto> list = new List<Produto>();
            foreach (var produto in produtos)
            {
                if (produto.ProdutoParaVenda == true)
                    list.Add(produto);
            }

            if (list.Count() <= 0 || list.Count() <= 0)
                throw new Exception("Antes de realizar alguma venda realize o cadastro de alguns produtos e clientes");

            var clientesViewModel = clientes.Select(x =>
                                                        new ClienteVendaViewModel(x.Id, x.Pessoa.Nome));
            var produtosViewModel = list.Select(x =>
                                                        new ProdutoVendaViewModel(x.Id, x.Nome, x.ValorPraticado));
            var cadastroVendaInputModel = new CadastroVendaInputModel()
            {
                Clientes = clientesViewModel,
                Produtos = produtosViewModel
            };

            return cadastroVendaInputModel;
        }

        public async Task<IEnumerable<RTicketMedioViewModel>> RTicketMedio(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            var vendas = await _vendaRepository.FiltrarVendasAsync(clienteId, periodo, inicial, final);

            List<RTicketMedioViewModel> list = new List<RTicketMedioViewModel>();
            decimal valorTotal = 0;
            decimal ticketMedio = 0;
            var quantidadeVendas = 0;
            var nomeCliente = "";

            if (clienteId == 0)
            {
                var clientesProcessados = new HashSet<int>(); 

                foreach (var venda in vendas)
                {
                    var id = venda.ClienteId;

                    if (clientesProcessados.Contains(id))
                    {
                        continue;
                    }

                    var vendasDoCliente = vendas.Where(v => v.ClienteId == id).ToList();
                    quantidadeVendas = vendasDoCliente.Count;
                    nomeCliente = vendasDoCliente.First().Cliente.Pessoa.Nome;
                    valorTotal = vendasDoCliente.Sum(v => (decimal)v.Total);
                    ticketMedio = valorTotal / quantidadeVendas;

                    var ticketMedioViewModel = new RTicketMedioViewModel(nomeCliente, quantidadeVendas, valorTotal, ticketMedio);
                    list.Add(ticketMedioViewModel);

                    clientesProcessados.Add(id);
                }
            }
            else
            {
                foreach (var venda in vendas)
                {
                    nomeCliente = venda.Cliente.Pessoa.Nome;
                    valorTotal += (decimal)venda.Total;
                    quantidadeVendas++;
                }

                if (quantidadeVendas > 0)
                {
                    ticketMedio = valorTotal / quantidadeVendas;
                }

                var ticketMedioViewModel = new RTicketMedioViewModel(nomeCliente, quantidadeVendas, valorTotal, ticketMedio);
                list.Add(ticketMedioViewModel);
            }

            return list;
        }

        public async Task<IEnumerable<RVendasPorPeriodoViewModel>> RVendasPorPeriodo(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            var vendas = await _vendaRepository.FiltrarVendasAsync(clienteId, periodo, inicial, final);

            List<RVendasPorPeriodoViewModel> viewModel = new List<RVendasPorPeriodoViewModel>();
            foreach (var venda in vendas)
            {
                var rVendasPorPeriodoViewModel = new RVendasPorPeriodoViewModel(venda.DataDaVenda, venda.Cliente.Pessoa.Nome, Enum.GetName(typeof(ETipoPagamento), venda.TipoPagamento), (decimal)venda.Total);
                viewModel.Add(rVendasPorPeriodoViewModel);
            }

            return viewModel;
        }

        public async Task SalvarVenda(CadastroVendaInputModel inputModel)
        {
            var venda = await _vendaRepository.GetVendaPorId(inputModel.VendaId);
            if (venda == null)
                throw new Exception("Venda não existe");

            decimal valorDesconto = 0;
            if (inputModel.ValorDesconto != null)
            {
                var culture = new CultureInfo("pt-BR");
                var desconto = decimal.Parse(inputModel.ValorDesconto.Replace("R$", "").Trim(), NumberStyles.Currency, culture);
                valorDesconto = desconto;
                venda.AplicarDesconto(valorDesconto);
            }

            venda.Update(Enum.Parse<ETipoPagamento>(inputModel.TipoPagamento), inputModel.ClienteId, valorDesconto);

            await _vendaRepository.AtualizarVendaAsync(venda);
        }

        private Expression<Func<T, bool>> AdicionarFiltro<T>(Expression<Func<T, bool>> existente, Expression<Func<T, bool>> novo)
        {
            var parametro = Expression.Parameter(typeof(T));

            var corpo = Expression.AndAlso(
                Expression.Invoke(existente, parametro),
                Expression.Invoke(novo, parametro)
            );

            return Expression.Lambda<Func<T, bool>>(corpo, parametro);
        }
    }
}
