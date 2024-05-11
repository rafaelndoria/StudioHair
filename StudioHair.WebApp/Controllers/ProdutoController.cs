using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Criar(int? id = 0)
        {
            var produtoInputModel = id == 0 ? new CadastroProdutoInputModel() : await _produtoService.GetProdutoInputPorId((int)id);
            return View(produtoInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroProdutoInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                if (inputModel.RedirecionarUnidade)
                    TempData["Erro"] = "Antes de adicionar unidades, preencha os campos necessarias para o cadastro do produto";

                return View("Criar", inputModel);
            }
            try
            {
                if (inputModel.Id != null)
                {
                    if (inputModel.RedirecionarUnidade)
                    {
                        TempData["Sucesso"] = "Produto salvo";
                        return RedirectToAction("Unidade", new { id = inputModel.Id });
                    }

                    await _produtoService.AtualizarProdutoInclusao(inputModel);

                    TempData["Sucesso"] = "Produto criado com sucesso";
                    return RedirectToAction("Criar", new { id = "" });
                }

                var produtoId = await _produtoService.CriarProduto(inputModel);
                if (inputModel.RedirecionarUnidade)
                {
                    TempData["Sucesso"] = "Produto salvo";
                    return RedirectToAction("Unidade", new { id = produtoId });
                }

                TempData["Sucesso"] = "Produto criado com sucesso";
                return RedirectToAction("Criar", new { id = "" });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao salvar produto: "+ ex.Message;
                return View("Criar", inputModel);
            }
        }

        public async Task<IActionResult> Unidade(int id)
        {
            try
            {
                var unidades = await _produtoService.GetUnidadesPorProdutoId(id);
                var inputModelProdutUnidade = new CadastroProdutoUnidadeInputModel() { ProdutoId = id };
                inputModelProdutUnidade.ProdutoUnidadeViewModel = unidades;
                return View(inputModelProdutUnidade);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir as unidades do produto: " + ex.Message;
                return RedirectToAction("Criar", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUnidade(CadastroProdutoUnidadeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Unidade", inputModel);
            }
            try
            {
                await _produtoService.CriarProdutoUnidade(inputModel);
                return RedirectToAction("Unidade", new { id = inputModel.ProdutoId });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao salvar a unidade: " + ex.Message;
                return View("Unidade", inputModel);
            }
        }

        public async Task<IActionResult> RemoverUnidade(int id, int produtoId, bool redirecionarConfig = false)
        {
            try
            {
                await _produtoService.DeletarUnidade(id);
                TempData["Sucesso"] = "Unidade deletada com sucesso";
                if (redirecionarConfig)
                {
                    return RedirectToAction("AtualizarUnidade", new { id = produtoId });
                }
                return RedirectToAction("Unidade", new { id = produtoId });
            }
            catch(Exception ex)
            {
                TempData["Erro"] = "Erro ao deletar unidade: "+ ex.Message;
                if (redirecionarConfig)
                {
                    return RedirectToAction("AtualizarUnidade", new { id = produtoId });
                }
                return RedirectToAction("Unidade", new { id = produtoId });
            }
        }

        public async Task<IActionResult> List()
        {
            var produtosViewModel = await _produtoService.GetProdutos();
            return View(produtosViewModel);
        }

        public async Task<IActionResult> Config(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoConfigPorId(id);
                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir as configurações do produto: "+ ex.Message;
                return RedirectToAction("List");
            }
        }

        public async Task<IActionResult> AtualizarUnidade(int id)
        {
            try
            {
                var unidades = await _produtoService.GetUnidadesPorProdutoId(id);
                var configUnidadeInputModel = new ConfiguracaoUnidadeInputModel() { ProdutoId = id };
                configUnidadeInputModel.ProdutoUnidadeViewModel = unidades;
                return View(configUnidadeInputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir unidades do produto: " + ex.Message;
                return RedirectToAction("Config", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAtualizarUnidade(ConfiguracaoUnidadeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AtualizarUnidade",inputModel);
            }
            try
            {
                // atualizar
                if (inputModel.Instrucao == 1)
                {
                    await _produtoService.AtualizarProdutoUnidade(inputModel);
                    TempData["Sucesso"] = "Unidade atualizada com sucesso";
                    return RedirectToAction("AtualizarUnidade", new { id = inputModel.ProdutoId });
                }
                // inserir
                else
                {
                    var cadastroProdutoUnidade = new CadastroProdutoUnidadeInputModel()
                    {
                        ProdutoId = inputModel.ProdutoId,
                        Quantidade = inputModel.Quantidade,
                        TipoUnidade = inputModel.TipoUnidade
                    };
                    await _produtoService.CriarProdutoUnidade(cadastroProdutoUnidade);
                    TempData["Sucesso"] = "Unidade criada com sucesso";
                    return RedirectToAction("AtualizarUnidade", new { id = inputModel.ProdutoId });
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao realizar manutenção na unidade: "+ ex.Message;
                return RedirectToAction("AtualizarUnidade", new { id = inputModel.ProdutoId });
            }
        }

        public async Task<IActionResult> Selecionar(int id, int produtoId)
        {
            try
            {
                var unidades = await _produtoService.GetUnidadesPorProdutoId(produtoId);
                var unidade = unidades.FirstOrDefault(x => x.Id == id);
                var configUnidadeInputModel = new ConfiguracaoUnidadeInputModel() { ProdutoId = produtoId, Id = id, Quantidade = unidade.Quantidade, TipoUnidade = unidade.TipoUnidade };
                configUnidadeInputModel.ProdutoUnidadeViewModel = unidades;
                return View("AtualizarUnidade", configUnidadeInputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao selecionar unidade: " + ex.Message;
                return RedirectToAction("AtualizarUnidade", new { id = produtoId });
            }
        }

        public async Task<IActionResult> Atualizar(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoParaAtualizar(id);
                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao selecionar produto para atualização: "+ ex.Message;
                return View("Config", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(AtualizarProdutoInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Atualizar", inputModel);
            }
            try
            {
                await _produtoService.AtualizarProduto(inputModel);
                TempData["Sucesso"] = "Produto alterado com sucesso";
                return View("Atualizar", inputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao alterar o produto: " + ex.Message;
                return View("Atualizar", inputModel);
            }
        }
    }
}
