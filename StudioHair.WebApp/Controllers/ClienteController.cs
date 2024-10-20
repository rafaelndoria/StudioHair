using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Criar()
        {
            try
            {
                var usuarios = await _clienteService.GetUsuarioSemVinculo();
                var inputModel = new CadastroPessoaInputModel();
                inputModel.Usuarios = usuarios;
                return View(inputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao processar formulario: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroPessoaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", inputModel);
            }
            try
            {
                var pessoaId = await _clienteService.CriarPessoa(inputModel);
                return RedirectToAction("CriarCliente", new { pessoaId = pessoaId });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao processar formulario: " + ex.Message;
                return RedirectToAction("Criar");
            }
        }

        public IActionResult CriarCliente(int pessoaId)
        {
            var cadastroClienteViewModel = new CadastroClienteInputModel() { PessoaId = pessoaId };
            return View("CriarCliente", cadastroClienteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente(CadastroClienteInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CriarCliente", inputModel);
            }
            try
            {       
                await _clienteService.CriarCliente(inputModel);
                TempData["Sucesso"] = "Cliente cadastrado com sucesso";
                return RedirectToAction("Criar");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ocorreu erro ao salvar cliente: " + ex.Message;
                await DeletarPessoa((int)inputModel.PessoaId);
                return RedirectToAction("Criar");
            }
        }

        public async Task<IActionResult> List(int page = 1, int pageSize = 5)
        {
            try
            {
                var clientesViewModel = await _clienteService.GetClientes(1, 9999999);
                return View(clientesViewModel);
            }
            catch
            {
                TempData["Erro"] = "Erro ao listar os clientes";
                return View();
            }
        }

        public async Task<IActionResult> Config(int id)
        {
            try
            {
                var clienteViewModel = await _clienteService.GetClienteConfig(id);
                // ajustar listagem
                return View(clienteViewModel);
            }
            catch
            {
                TempData["Erro"] = "Erro ao consultar cliente";
                return View("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Inativar(int id)
        {
            try
            {
                await _clienteService.InativarCliente(id);
                TempData["Sucesso"] = "Cliente Inativado com sucesso";
                return RedirectToAction("Config", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao inativar cliente: "+ ex.Message;
                return RedirectToAction("Config", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ativar(int id)
        {
            try
            {
                await _clienteService.AtivarCliente(id);
                TempData["Sucesso"] = "Cliente ativado com sucesso";
                return RedirectToAction("Config", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao ativar cliente: " + ex.Message;
                return RedirectToAction("Config", new { id = id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            var updateCliente = await _clienteService.GetClienteUpdate(id);
            return View(updateCliente);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(UpdateClienteInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Atualizar", new { id = inputModel.Id });
            }
            try
            {
                await _clienteService.AtualizarCliente(inputModel);
                TempData["Sucesso"] = "Cliente atualizado com sucesso";
                return RedirectToAction("Config", new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar cliente: "+ ex.Message;
                return RedirectToAction("Atualizar", new { id = inputModel.Id });
            }
        }

        private async Task DeletarPessoa(int pessoaId)
        {
            await _clienteService.DeletarPessoa(pessoaId);
        }
    }
}
