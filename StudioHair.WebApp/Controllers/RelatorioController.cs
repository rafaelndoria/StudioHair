using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.WebApp.Controllers
{
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
            var filtroRelatorio = await PrepararFiltroVendas();
            return View(filtroRelatorio);
        }

        [HttpPost]
        public async Task<IActionResult> VendasPorPeriodo(FiltroRelatorioVendasInputModel inputModel)
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
            var filtroRelatorio = await PrepararFiltroVendas();
            return View(filtroRelatorio);
        }

        [HttpPost]
        public async Task<IActionResult> TicketMedio(FiltroRelatorioVendasInputModel inputModel)
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


        private async Task<FiltroRelatorioVendasInputModel> PrepararFiltroVendas()
        {
            var clientes = await _clienteService.GetClientes(1, 999999);
            var filtroRelatorio = new FiltroRelatorioVendasInputModel() { Clientes = clientes };
            return filtroRelatorio;
        }
    }
}