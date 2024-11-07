using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class VendaController : BaseController
    {
        private readonly IVendaService _vendaService;
        private readonly IProdutoService _produtoService;

        public VendaController(IVendaService vendaService, IProdutoService produtoService)
        {
            _vendaService = vendaService;
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Criar()
        {
            try
            {
                var inputVendas = await _vendaService.PrepararVendas();
                return View(inputVendas);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir as vendas: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroVendaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Preencha todos campos do produto e cliente obrigatórios";
                return RedirectToAction("Criar");
            }
            try
            {
                if (inputModel.AdicionarProdutoVenda == 2)
                {
                    if (inputModel.VendaId == 0 || inputModel.VendaId == null)
                    {
                        var vendaId = await _vendaService.CriarVenda(inputModel);
                        inputModel.VendaId = vendaId;
                    }

                    var clientesProdutos = await _vendaService.PrepararVendas();
                    inputModel.Clientes = clientesProdutos.Clientes;
                    inputModel.Produtos = clientesProdutos.Produtos;

                    var valorTotal = await _vendaService.AdicionarProdutoVenda(inputModel);
                    inputModel.ValorTotal = valorTotal.ToString();

                    var produtosVenda = await _vendaService.GetProdutosVenda(inputModel.VendaId);
                    inputModel.ProdutosVenda = produtosVenda;

                    return View("Criar", inputModel);
                }
                else
                {
                    await _vendaService.SalvarVenda(inputModel);
                    TempData["Sucesso"] = "Venda realizada com sucesso";
                    return RedirectToAction("Criar");
                }
            }
            catch (Exception ex)
            {
                if (inputModel.VendaId != 0)
                    await _vendaService.DeletarVenda(inputModel.VendaId);
                TempData["Erro"] = "Erro ao incluir a venda: " + ex.Message;
                return RedirectToAction("Criar");
            }
        }

        public async Task<IActionResult> List()
        {
            try
            {
                var clientes = await _vendaService.PrepararVendas();
                var inputModel = new FiltroListVendasInputModel()
                {
                    Clientes = clientes.Clientes
                };

                return View(inputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir a listagem das vendas: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> List(FiltroListVendasInputModel inputModel)
        {
            try
            {
                var vendasFiltradas = await _vendaService.FiltrarVendas(inputModel);
                return View("ListVendas", vendasFiltradas);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao filtrar as vendas: " + ex.Message;
                return View("List", inputModel);
            }
        }

        public async Task<IActionResult> Config(int id)
        {
            try
            {
                var detalhesVenda = await _vendaService.GetDetalhesVenda(id);
                return View(detalhesVenda);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir os detalhes da venda: " + ex.Message;
                return View("List");
            }
        }

        public async Task<IActionResult> Catalogo(int tamanhoPagina = 10, int paginaAtual = 1, string query = "")
        {
            try
            {
                var produtosViewModel = await _produtoService.GetProdutosCatalogo(tamanhoPagina, paginaAtual, query);
                return View(produtosViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir o catálogo de produtos: " + ex.Message;
                return RedirectToAction("Home", "IndexCliente");
            }
        }

        public async Task<IActionResult> DetalhesProduto(int produtoId)
        {
            try
            {
                var produtoDetalhesViewModel = await _produtoService.GetDetalhesProduto(produtoId);
                return View(produtoDetalhesViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir os detalhes do produto: " + ex.Message;
                return RedirectToAction("Catalogo");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAoCarrinho([FromBody] AdicionarAoCarrinhoInputModel request)
        {
            try
            {
                if (request == null || request.ProdutoId <= 0 || request.Quantidade <= 0)
                {
                    return Json(new { sucesso = false, mensagem = "Dados inválidos." });
                }

                var carrinhoClienteId = int.Parse(HttpContext.Session.GetString("CarrinhoId"));
                var carrinho = await _vendaService.AdicionarItemCarrinho(request, carrinhoClienteId);
                // Atualize o valor na sessão
                HttpContext.Session.SetString("QuantidadeItensCarrinho", carrinho.CarrinhoItems.Count.ToString());

                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao adicionado o produto no carrinho: " + ex.Message });
            }
        }

        public async Task<IActionResult> Carrinho()
        {
            try
            {
                var carrinhoViewModel = await _vendaService.GetCarrinhoDetalhes(int.Parse(HttpContext.Session.GetString("CarrinhoId")));
                return View(carrinhoViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir o carrinho de produtos: " + ex.Message;
                return RedirectToAction("Catalogo");
            }
        }

        public async Task<IActionResult> FinalizarCarrinho()
        {
            try
            {
                await _vendaService.FinalizarCarrinho(int.Parse(HttpContext.Session.GetString("CarrinhoId")), int.Parse(HttpContext.Session.GetString("ClienteId")));

                TempData["Ok"] = "Compra finalizada com sucesso";
                return RedirectToAction("Catalogo");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro finalizar o carrinho, tente novamente mais tarde: " + ex.Message;
                return RedirectToAction("Carrinho");
            }
        }

        public async Task<IActionResult> ExcluirItemCarrinho(int produtoId)
        {
            try
            {
                var carrinhoId = int.Parse(HttpContext.Session.GetString("CarrinhoId"));
                await _vendaService.ExcluirProdutoCarrinho(produtoId, carrinhoId);
                return RedirectToAction("Carrinho");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao excluir o item do carrinho: " + ex.Message;
                return RedirectToAction("Carrinho");
            }
        }
    }
}
