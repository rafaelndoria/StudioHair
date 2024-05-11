using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private readonly IServicoService _servicoService;

        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroServicoInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", inputModel);
            }
            try
            {
                await _servicoService.CriarServico(inputModel);
                TempData["Sucesso"] = "Serviço criado com sucesso";
                return View("Criar", inputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao criar o serviço: "+ ex.Message;
                return View("Criar", inputModel);
            }
        }

        public async Task<IActionResult> List()
        {
            try
            {
                var servicosViewModel = await _servicoService.GetServicos();
                return View(servicosViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao carregar os serviços: " + ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Config(int id)
        {
            try
            {
                var servicoViewModel = await _servicoService.GetServicoConfig(id);
                return View(servicoViewModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir configuração do serviço: " + ex.Message;
                return View("List");
            }
        }

        public async Task<IActionResult> Atualizar(int id)
        {
            try
            {
                var updateServicoInputModel = await _servicoService.GetServicoParaUpdate(id);
                return View(updateServicoInputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir atualização do serviço: " + ex.Message;
                return RedirectToAction("Config", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(AtualizarServicoInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Atualizar", inputModel);
            }
            try
            {
                await _servicoService.AtualziarServico(inputModel);
                TempData["Sucesso"] = "Serviço atualizado com sucesso";
                return RedirectToAction("Atualizar", new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar serviço: " + ex.Message;
                return RedirectToAction("Atualizar", new { id = inputModel.Id });
            }
        }
    }
}
