using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize]
    public class AgendamentoController : BaseController
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        public async Task<IActionResult> Criar()
        {
            try
            {
                var inputModel = await _agendamentoService.PrepararAgendamento();
                return View(inputModel);
            }
            catch(Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir o agendamento "+ ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CadastroAgendamentoInputModel inputModel)
        {
            try
            {
                // adicionar servico
                 if (inputModel.AdicionarServico == 2)
                {
                    ValidarCadastroServico(inputModel);
                    if (inputModel.AgendamentoId <= 0 || inputModel.AgendamentoId == null)
                    {
                        var id = await _agendamentoService.CriarAgendamentoProv(inputModel);
                        inputModel.AgendamentoId = id;
                    }

                    await _agendamentoService.AdicionarServicoAgendamento(inputModel);

                    var listas = await _agendamentoService.PrepararAgendamento();
                    var listServicos = await _agendamentoService.ListProdutosAgendamento(inputModel.AgendamentoId);

                    inputModel.Servicos = listas.Servicos;
                    inputModel.Clientes = listas.Clientes;
                    inputModel.ServicosAgendamentos = listServicos;

                    ModelState.Clear();

                    return View("Criar", inputModel);
                }
                // salvar/atualizar agendamento
                else
                {
                    if (!ModelState.IsValid)
                    {
                        TempData["Erro"] = "Preencha os campos obrigatorios";
                        var listas = await _agendamentoService.PrepararAgendamento();
                        var listServicos = await _agendamentoService.ListProdutosAgendamento(inputModel.AgendamentoId);

                        inputModel.Servicos = listas.Servicos;
                        inputModel.Clientes = listas.Clientes;
                        inputModel.ServicosAgendamentos = listServicos;
                        return View("Criar", inputModel);
                    }
                    if (!await _agendamentoService.VerificarDisponibilizadaAgendamento(inputModel))
                    {
                        TempData["Erro"] = "Ja existe agendamento marcado nesse horario e dia, escolha outro horario para prosseguir";

                        var listas = await _agendamentoService.PrepararAgendamento();
                        var listServicos = await _agendamentoService.ListProdutosAgendamento(inputModel.AgendamentoId);

                        inputModel.Servicos = listas.Servicos;
                        inputModel.Clientes = listas.Clientes;
                        inputModel.ServicosAgendamentos = listServicos;

                        return View("Criar", inputModel);
                    }
                    
                    await _agendamentoService.SalvarAgendamento(inputModel);

                    TempData["Sucesso"] = "Agendamento Criado com sucesso";
                    return RedirectToAction("Criar");
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao criar agendamento: " + ex.Message;
                return RedirectToAction("Criar");
            }
        }

        public async Task<IActionResult> List()
        {
            var agendamentosViewModel = await _agendamentoService.GetAgendamentos();
            return View(agendamentosViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(FiltroAgendamentosInputModel inputModel)
        {
            var agendamentosFiltrados = await _agendamentoService.FiltrarAgendamentos(inputModel);
            return View("ListAgendamentos", agendamentosFiltrados);
        }

        public async Task<IActionResult> Config(int id)
        {
            var agendamentoDetalheViewModel = await _agendamentoService.GetDetalheAgendamento(id);
            return View(agendamentoDetalheViewModel);
        }

        public IActionResult DataDisponivel()
        {
            var inputModel = new AgendamentoClienteInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> DataDisponivel(AgendamentoClienteInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("DataDisponivel", inputModel);
            }
            try
            {
                var horariosDisponiveis = await _agendamentoService.GetHorariosDisponiveis(inputModel.Dia);
                horariosDisponiveis.DataEscolhida = inputModel.Dia;
                return View("DataDisponivel", horariosDisponiveis);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao consultar os agendamentos: " + ex.Message;
                return RedirectToAction("DataDisponivel");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PrepararAgendamento(AgendamentoClienteInputModel inputModel)
        {
            try
            {
                var listas = await _agendamentoService.PrepararAgendamento();
                var agendamentoInputModel = new CriarAgendamentoClienteInputModel()
                {
                    Dia = inputModel.DataEscolhida,
                    HoraInicial = inputModel.HoraInicial,
                    Servicos = listas.Servicos,
                    UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"))
                };
                return View(agendamentoInputModel);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao abrir o agendamento: " + ex.Message;
                return RedirectToAction("DataDisponivel");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarAgendamentoCliente(CriarAgendamentoClienteInputModel inputModel)
        {
            try
            {
                var agendamentoId = await _agendamentoService.CriarAgendamentoCliente(inputModel);
                var resumoAgendamento = await _agendamentoService.GetResumoAgendamento(agendamentoId);
               
                return View("ConfirmarAgendamento", resumoAgendamento);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro inserir agendamento, tente novamente: " + ex.Message;
                return RedirectToAction("DataDisponivel");
            }
        }

        public async Task<IActionResult> ConfirmarAgendamento(int agendamentoId)
        {
            try
            {
                await _agendamentoService.ConfirmarAgendamento(agendamentoId);
                TempData["Sucesso"] = "Agendamento confirmardo com sucesso.";
                return RedirectToAction("DataDisponivel");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao confirmar o agendamento: "+ ex.Message;
                return RedirectToAction("DataDisponivel");
            }
        }

        private void ValidarCadastroServico(CadastroAgendamentoInputModel inputModel)
        {
            var valido = true;
            if (inputModel.ClienteId == null || inputModel.ClienteId <= 0)
                valido = false;
            if (!valido)
                throw new Exception("Antes de adicionar o serviço, primeiro informe o cliente");
        }
    }
}
