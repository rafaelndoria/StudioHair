using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginInputModel inputModel)
        { 
            if (!ModelState.IsValid)
            {
                return View("Login", inputModel);
                
            }
            var token = await _usuarioService.Login(inputModel);
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha incorretos");
                return View("Login", inputModel);
            }
            HttpContext.Session.SetString("Token", token);
            return RedirectToAction("Index", "Home");
        }
    }
}
