using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Implementations
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IVendaService _vendaService;
        private readonly IClienteService _clienteService;
        private readonly IAgendamentoService _agendamentoService;

        public RelatorioService(IVendaService vendaService,
                                IClienteService clienteService,
                                IAgendamentoService agendamentoService)
        {
            _vendaService = vendaService;
            _clienteService = clienteService;
            _agendamentoService = agendamentoService;
        }

        public async Task<FiltrosViewModel> GetFiltros(int clienteId, string periodo, DateTime inicial, DateTime final)
        {
            var filtros = "Clientes: ";
            if (clienteId == 0)
            {
                var clientes = await _clienteService.GetClientes(1, 9999999);
                foreach (var cliente in clientes)
                {
                    filtros += cliente.Codigo + ", ";
                }
            }
            else
            {
                filtros += "Clientes: " + clienteId;
            }

            if (periodo == "todos")
            {
                filtros += "Data: Todos";
            }
            else if (periodo == "dia")
            {
                filtros += "Data: " + DateTime.Now;
            }
            else
            {
                filtros += "Data: " + inicial.ToString("dd/MM/yyyy") + " - " + final.ToString("dd/MM/yyyy");
            }
            var filtro = new FiltrosViewModel(filtros);
            return filtro;
        }

        public async Task<RelatorioFrequenciaSalaoViewModel> RelatorioFrequenciaSalao(FiltroRelatorioVAInputModel inputModel)
        {
            var dados = await _agendamentoService.RFrequenciaSalao(inputModel);
            var filtros = await GetFiltros(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);

            var relatorioViewModel = new RelatorioFrequenciaSalaoViewModel(filtros.Filtros, "admin");
            relatorioViewModel.FrequenciasSalao = dados;

            return relatorioViewModel;
        }

        public async Task<RelatorioPeriodoAgendamentosViewModel> RelatorioPeriodoAgendamentos(FiltroRelatorioVAInputModel inputModel)
        {
            var dados = await _agendamentoService.RPeriodoAgendamentos(inputModel);
            var filtros = await GetFiltros(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);

            var relatorioViewModel = new RelatorioPeriodoAgendamentosViewModel(filtros.Filtros, "admin");
            relatorioViewModel.AgendamentosPorPeriodo = dados;
            return relatorioViewModel;
        }

        public async Task<RelatorioPeriodoVendasViewModel> RelatorioPeriodoVendas(FiltroRelatorioVAInputModel inputModel)
        {
            var vendas = await _vendaService.RVendasPorPeriodo(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);

            var filtros = await GetFiltros(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);
           
            var relatorioViewModel = new RelatorioPeriodoVendasViewModel(filtros.Filtros, "adm");
            relatorioViewModel.VendasPorPeriodo = vendas;

            return relatorioViewModel;
        }

        public async Task<RelatorioTicketMedioViewModel> RelatorioTicketMedio(FiltroRelatorioVAInputModel inputModel)
        {
            var ticketMedio = await _vendaService.RTicketMedio(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);
            var filtros = await GetFiltros(inputModel.ClienteId, inputModel.Periodo, inputModel.Inicial, inputModel.Final);
            var relatorioViewModel = new RelatorioTicketMedioViewModel(filtros.Filtros, "admin");
            relatorioViewModel.TicketMedio = ticketMedio;
            return relatorioViewModel;
        }
    }
}
