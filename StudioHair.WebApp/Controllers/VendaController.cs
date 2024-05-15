using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class VendaController : Controller
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
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
                TempData["Erro"] = "Erro ao abrir a listagem das vendas: "+ ex.Message;
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
                TempData["Erro"] = "Erro ao filtrar as vendas: "+ ex.Message;
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
                TempData["Erro"] = "Erro ao abrir os detalhes da venda: "+ ex.Message;
                return View("List");
            }
        }
    }
}
