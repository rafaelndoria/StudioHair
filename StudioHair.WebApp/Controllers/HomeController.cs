using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.Services.Interfaces;
using StudioHair.WebApp.Models;
using System.Diagnostics;

namespace StudioHair.WebApp.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Funcionario, Cliente")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IUsuarioService _usuarioService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService, IUsuarioService usuarioService)
        {
            _logger = logger;
            _homeService = homeService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var resumoViewModel = await _homeService.PrepararResumo();
            return View(resumoViewModel);
        }

        public async Task<IActionResult> IndexCliente()
        {
            var usuario = await _usuarioService.GetUsuarioLogado(User);
            HttpContext.Session.SetString("ClienteId", usuario.Id.ToString());

            var configs = await _usuarioService.GetConfigSistemaAsync(usuario.Id);
            // Salvar as configurações na sessão
            HttpContext.Session.SetString("CorPrimaria", configs.CorPrimaria ?? "#ffc0cb");
            HttpContext.Session.SetString("CorSecundaria", configs.CorSecundaria ?? "#f8f9fa");
            HttpContext.Session.SetString("CorFonte", configs.CorFonte ?? "#212529");
            HttpContext.Session.SetString("TamanhoFonte", configs.TamanhoFonte > 0 ? configs.TamanhoFonte.ToString() : "16");
            HttpContext.Session.SetString("TemaDark", configs.TemaDark ? "true" : "false");

            if (usuario.Pessoa == null)
                return View("IndexClienteBloqueado");
            if (usuario.Pessoa.Cliente == null)
                return RedirectToAction("CadastroCliente", "Usuario");

            return RedirectToAction("TelaCliente");
        }

        public IActionResult TelaCliente()
        {
               return View("IndexCliente");   
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}