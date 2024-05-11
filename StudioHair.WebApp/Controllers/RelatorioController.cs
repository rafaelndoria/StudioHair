using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
    [Authorize(Roles = "Administrador, Gerente")]
    public class RelatorioController : Controller   
    {
        private IClienteService _clienteService;
        private IRelatorioService _relatorioService;

        public RelatorioController(IClienteService clienteService, IRelatorioService relatorioService)
        {
            _clienteService = clienteService;
            _relatorioService = relatorioService;
        }


        #region Vendas

        public async Task<IActionResult> VendasPorPeriodo()
        {
            var filtroRelatorio = await PrepararFiltroVendasAgendamentosRelatorio();
            return View(filtroRelatorio);
        }

        [HttpPost]
        public async Task<IActionResult> VendasPorPeriodo(FiltroRelatorioVAInputModel inputModel)
        {
            try
            {
                var dadosRelatorio = await _relatorioService.RelatorioPeriodoVendas(inputModel);
                return View("RVendasPorPeriodo", dadosRelatorio);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao emitir o relatório: " + ex.Message;
                return RedirectToAction("VendasPorPeriodo");
            }
        }

        public async Task<IActionResult> TicketMedio()
        {
            var filtroRelatorio = await PrepararFiltroVendasAgendamentosRelatorio();
            return View(filtroRelatorio);
        }

        [HttpPost]
        public async Task<IActionResult> TicketMedio(FiltroRelatorioVAInputModel inputModel)
        {
            try
            {
                var dadosRelatorio = await _relatorioService.RelatorioTicketMedio(inputModel);
                return View("RTicketMedio", dadosRelatorio);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao emitir o relatório: " + ex.Message;
                return RedirectToAction("TicketMedio");
            }
        }

        #endregion

        #region Agendamentos

        public async Task<IActionResult> AgendamentoPorPeriodo()
        {
            var filtro = await PrepararFiltroVendasAgendamentosRelatorio();
            return View(filtro);
        }

        [HttpPost]
        public async Task<IActionResult> AgendamentoPorPeriodo(FiltroRelatorioVAInputModel inputModel)
        {
            try
            {
                var dadosRelatorio = await _relatorioService.RelatorioPeriodoAgendamentos(inputModel);
                return View("RAgendamentoPorPeriodo", dadosRelatorio);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao emitir o relatório: " + ex.Message;
                return RedirectToAction("AgendamentoPorPeriodo");
            }
        }

        public async Task<IActionResult> FrequenciaSalao()
        {
            var filtro = await PrepararFiltroVendasAgendamentosRelatorio();
            return View(filtro);
        }

        [HttpPost]
        public async Task<IActionResult> FrequenciaSalao(FiltroRelatorioVAInputModel inputModel)
        {
            try
            {
                var dados = await _relatorioService.RelatorioFrequenciaSalao(inputModel);
                return View("RFrequenciaSalao", dados);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao emitir o relatório: " + ex.Message;
                return RedirectToAction("FrequenciaSalao");
            }
        }

        #endregion

        private async Task<FiltroRelatorioVAInputModel> PrepararFiltroVendasAgendamentosRelatorio()
        {
            var clientes = await _clienteService.GetClientes(1, 999999);
            var filtroRelatorio = new FiltroRelatorioVAInputModel() { Clientes = clientes };
            return filtroRelatorio;
        }
    }
}