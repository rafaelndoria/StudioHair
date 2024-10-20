using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IClienteService _clienteService;

        public UsuarioController(IUsuarioService usuarioService, IClienteService clienteService)
        {
            _usuarioService = usuarioService;
            _clienteService = clienteService;
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
 
                var papel = await _usuarioService.GetPapelUsuario(inputModel);
                if (papel != Core.Enums.EPapelUsuario.Cliente)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("IndexCliente", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao autenticar usuário: "+ ex.Message);
                return View("Login", inputModel);
            }
        }

        [AllowAnonymous]
        public IActionResult CriarConta()
        {
            return View();
        }

        [Authorize(Roles = "Gerente, Administrador")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Administrador")]
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario(CadastroUsuarioInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CriarConta", inputModel);
            }

            try
            {
                await _usuarioService.CriarUsuario(inputModel);
                TempData["Sucesso"] = "Usuário adicionado com sucesso!";
                return View("Login");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao criar o usuário. Por favor, tente novamente. Detalhe: " + ex.Message);
                return View("CriarConta", inputModel);
            }
        }

        [Authorize(Roles = "Gerente, Administrador")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 5)
        {
            var usuariosViewModel = await _usuarioService.GetUsuarios(1, 99999999);
            return View(usuariosViewModel);
        }

        [Authorize(Roles = "Gerente, Administrador")]
        public async Task<IActionResult> Config(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            return View(usuario);
        }

        [Authorize(Roles = "Gerente, Administrador")]
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

        [Authorize(Roles = "Gerente, Administrador")]
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
        [Authorize(Roles = "Gerente, Administrador")]
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
        [Authorize(Roles = "Gerente, Administrador")]
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
        [Authorize(Roles = "Gerente, Administrador")]
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

        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public IActionResult CadastroPessoa()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public async Task<IActionResult> CadastroPessoa(CadastroPessoaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CadastroPessoa", inputModel);
            }
            try
            {
                inputModel.UsuarioId = int.Parse(HttpContext.Session.GetString("ClienteId"));
                await _clienteService.CriarPessoa(inputModel);
                return RedirectToAction("CadastroCliente");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao criar o usuário. Por favor, tente novamente. Detalhe: " + ex.Message);
                return View("CadastroPessoa", inputModel);
            }
        }

        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public IActionResult CadastroCliente()
        {
            return View("CadastroCliente");
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public async Task<IActionResult> CadastroCliente(CadastroClienteInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CadastroCliente", inputModel);
            }
            try
            {
                var usuario = await _usuarioService.GetUsuarioLogado(User);
                inputModel.PessoaId = usuario.Pessoa.Id;
                await _clienteService.CriarCliente(inputModel);
                return RedirectToAction("TelaCliente", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao criar o usuário. Por favor, tente novamente. Detalhe: " + ex.Message);
                return View("CadastroCliente", inputModel);
            }
        }

        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public async Task<IActionResult> ConfiguracaoSistema()
        {
            try
            {
                var usuarioId = int.Parse(HttpContext.Session.GetString("ClienteId"));
                var configsInputModel = await _usuarioService.GetConfigSistemaAsync(usuarioId);
                return View(configsInputModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao abrir formulario. Por favor, tente novamente. Detalhe: " + ex.Message);
                return RedirectToAction("TelaCliente", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Administrador, Cliente")]
        public async Task<IActionResult> ConfiguracaoSistema(ConfigSistemaInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfiguracaoSistema", inputModel);
            }
            try
            {
                await _usuarioService.UpdateConfigSistema(inputModel);
                TempData["Sucesso"] = "Configuraçao salva com suceso";
                // Salvar as configurações na sessão
                HttpContext.Session.SetString("CorPrimaria", inputModel.CorPrimaria ?? "#ffc0cb");
                HttpContext.Session.SetString("CorSecundaria", inputModel.CorSecundaria ?? "#f8f9fa");
                HttpContext.Session.SetString("CorFonte", inputModel.CorFonte ?? "#212529");
                HttpContext.Session.SetString("TamanhoFonte", inputModel.TamanhoFonte > 0 ? inputModel.TamanhoFonte.ToString() : "16");
                HttpContext.Session.SetString("TemaDark", inputModel.TemaDark ? "true" : "false");
                return RedirectToAction("ConfiguracaoSistema");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao atualizar as configurações. Por favor, tente novamente. Detalhe: " + ex.Message);
                return View("ConfiguracaoSistema", inputModel);
            }
        }

        public IActionResult Logout()
        {
            // implementar logica para logout de usuario
            return RedirectToAction("Login");
        }
    }
}
