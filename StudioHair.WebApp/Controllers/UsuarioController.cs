using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize(Roles = "Gerente, Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginInputModel inputModel)
        { 
            if (!ModelState.IsValid)
            {
                return View("Login", inputModel);    
            }
            try
            {
                var token = await _usuarioService.Login(inputModel);
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao autenticar usuário: "+ ex.Message);
                return View("Login", inputModel);
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroUsuarioInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", inputModel);
            }

            try
            {
                await _usuarioService.CriarUsuario(inputModel);
                TempData["Sucesso"] = "Usuário adicionado com sucesso!";
                return View("Criar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao criar o usuário. Por favor, tente novamente. Detalhe: "+ ex.Message);
                return View("Criar", inputModel);
            }
        }

        public async Task<IActionResult> List(int page = 1, int pageSize = 5)
        {
            var usuariosViewModel = await _usuarioService.GetUsuarios(page, pageSize);
            var proximaPagina = await _usuarioService.VerificarProximaPagina(page, pageSize);
            // reformular paginação
            // informar dois números padrões 1, número da página anterior, número da proxima página, número da proxima página, .., número da ultima página
            ViewData["PageNumber"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["HasNextPage"] = proximaPagina;
            return View(usuariosViewModel);
        }

        public async Task<IActionResult> Config(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            return View(usuario);
        }

        public async Task<IActionResult> Inativar(int id)
        {
            try
            {
                await _usuarioService.InativarUsuario(id);
                TempData["Sucesso"] = "Usuário inativado";
                return RedirectToAction("Config", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao inativar usuário "+ ex.Message;
                return RedirectToAction("Config", new { id = id});
            }
        }

        public async Task<IActionResult> Ativar(int id)
        {
            try
            {
                await _usuarioService.AtivarUsuario(id);
                TempData["Sucesso"] = "Usuário ativado";
                return RedirectToAction("Config", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao ativar usuário " + ex.Message;
                return RedirectToAction("Config", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Erro ao alterar senha";
                return RedirectToAction("Config", new { id = inputModel.Id });
            }

            try
            {
                await _usuarioService.RedefinirSenha(inputModel);
                TempData["Sucesso"] = "Senha redefinida";
                return RedirectToAction("Config", new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao alterar senha: " + ex.Message;
                return RedirectToAction("Config", new { id = inputModel.Id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            var usuario = await _usuarioService.GetUsuarioUpdate(id);
            if (usuario == null)
            {
                TempData["Erro"] = "Erro ao carregar o usuário";
                return RedirectToAction("Config", new { id = id });
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(UpdateUsuarioInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Atualizar", inputModel);
            }
            try
            {
                await _usuarioService.UpdateUsuario(inputModel);
                TempData["Sucesso"] = "Usuário atualizado com sucesso";
                return RedirectToAction("Atualizar", new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao atualizar o usuário. Por favor, tente novamente. Detalhe: " + ex.Message);
                return View("Config", inputModel);
            }
        }

        public IActionResult Logout()
        {
            // implementar logica para logout de usuario
            return RedirectToAction("Login");
        }
        // enum não esta indo certo na tela de config
    }
}
