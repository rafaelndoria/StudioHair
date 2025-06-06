﻿using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IVendaService
    {
        Task<CadastroVendaInputModel> PrepararVendas();
        Task<int> CriarVenda(CadastroVendaInputModel inputModel);
        Task<decimal> AdicionarProdutoVenda(CadastroVendaInputModel inputModel);
        Task<List<ProdutosVendaViewModel>> GetProdutosVenda(int id);
        Task SalvarVenda(CadastroVendaInputModel inputModel);
        Task DeletarVenda(int id);
        Task<IEnumerable<VendasViewModel>> FiltrarVendas(FiltroListVendasInputModel inputModel);
        Task<DetalhesVendaViewModel> GetDetalhesVenda(int id);
        Task<IEnumerable<RVendasPorPeriodoViewModel>> RVendasPorPeriodo(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<IEnumerable<RTicketMedioViewModel>> RTicketMedio(int clienteId, string periodo, DateTime inicial, DateTime final);
        Task<int> CriarCarrinho(int clienteId);
        Task<Carrinho> GetCarrinhoPorClienteId(int clienteId);
        Task<Carrinho> AdicionarItemCarrinho(AdicionarAoCarrinhoInputModel inputModel, int carrinhoId);
        Task<CarrinhoDetalhesViewModel> GetCarrinhoDetalhes(int carrinhoId);
        Task FinalizarCarrinho(int carrinhoId, int clienteId);
        Task EsvaziarItensCarrinho(int carrinhoId);
        Task ExcluirProdutoCarrinho(int produtoId, int carrinhoId);
    }
}
