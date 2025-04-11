using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Enums;

namespace StudioHair.WebApp.Controllers
{
    [Authorize(Roles = "Administrador, Gerente")]
    public class AnaliseGerencialController : BaseController
    {
        private readonly IAnaliseGerencialService _analiseGerencialService;

        public AnaliseGerencialController(IAnaliseGerencialService analiseGerencialService)
        {
            _analiseGerencialService = analiseGerencialService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Filtro(ETipoAnaliseGerencial tipoAnaliseGerencial)
        {
            var filtroAnalisesInputModel = await _analiseGerencialService.ObterFiltroAnalises(tipoAnaliseGerencial);
            filtroAnalisesInputModel.DescricaoRelatorio = ObterDescricaoRelatorio(tipoAnaliseGerencial);
            filtroAnalisesInputModel.TipoAnalise = tipoAnaliseGerencial;
            return View(filtroAnalisesInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> GerarAnalise(FiltroAnaliseInputModel inputModel, string exportType)
        {
            ModelState.Clear();
            if (!string.IsNullOrEmpty(exportType))
            {
                await ExportarAnalise(exportType, inputModel.TipoAnalise);
                return Ok();
            }
            else
            {
                var dados = await GerarAnalise(inputModel);
                return View(dados.NomeViewAnalise, dados);
            }
        }

        private string ObterDescricaoRelatorio(ETipoAnaliseGerencial tipoAnaliseGerencial)
        {
            string descricaoRelatorio = "";
            switch (tipoAnaliseGerencial)
            {
                case ETipoAnaliseGerencial.FaturamentoMensal:
                    descricaoRelatorio = "Faturamento Mensal";
                    break;
                case ETipoAnaliseGerencial.ProdutosMaisVendidos:
                    descricaoRelatorio = "Produtos Mais Vendidos";
                    break;
                case ETipoAnaliseGerencial.TicketMedioCliente:
                    descricaoRelatorio = "Ticket Medio Cliente";
                    break;
                case ETipoAnaliseGerencial.ClientesFrequentes:
                    descricaoRelatorio = "Clientes Frequentes";
                    break;
                case ETipoAnaliseGerencial.ServicosMaisVendidos:
                    descricaoRelatorio = "Serviços Mais Vendidos";
                    break;
                case ETipoAnaliseGerencial.HorarioDePico:
                    descricaoRelatorio = "Horario de Pico";
                    break;
                default:
                    descricaoRelatorio = "Filtros de Relatório";
                    break;
            }

            return descricaoRelatorio;
        }

        private Task ExportarAnalise(string tipoExportacao, ETipoAnaliseGerencial tipoAnaliseGerencial)
        {
            throw new Exception();
        }

        private async Task<DadosAnaliseViewModel> GerarAnalise(FiltroAnaliseInputModel inputModel)
        {
            var dadosViewModel = await _analiseGerencialService.BuscarDadosAnalise(inputModel);
            return dadosViewModel;
        }
    }
}
