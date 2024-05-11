using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;

namespace StudioHair.Application.Services.Implementations
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IClienteRepository _clienteRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IServicoRepository servicoRepository, IClienteRepository clienteRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _servicoRepository = servicoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task AdicionarServicoAgendamento(CadastroAgendamentoInputModel inputModel)
        {
            var servicoAgendamento = new AgendamentoServicos(inputModel.AgendamentoId, inputModel.ServicoId);
            await _agendamentoRepository.AdicionarServicoAgendamento(servicoAgendamento);
        }

        public async Task<int> CriarAgendamentoProv(CadastroAgendamentoInputModel inputModel)
        {
            var agendamento = new Agendamento("", DateTime.MinValue, "", "", 0, inputModel.ClienteId, EAgendamento.Pendente);
            var idAgendamento = await _agendamentoRepository.CriarAgendamento(agendamento);
            return idAgendamento;
        }

        public async Task<IEnumerable<AgendamentosViewModel>> FiltrarAgendamentos(FiltroAgendamentosInputModel inputModel)
        {
            List<Agendamento> list = new List<Agendamento>();
            switch (inputModel.Periodo)
            {
                case "dia":
                    var diaAtual = DateTime.Now;
                    list = await _agendamentoRepository.GetAgendamentosAsync(x => x.Dia >= diaAtual || x.Dia <= diaAtual);
                    break;
                case "intervalo":
                    list = await _agendamentoRepository.GetAgendamentosAsync(x => x.Dia >= inputModel.Inicial || x.Dia <= inputModel.Final);
                    break;
                default:
                    list = await _agendamentoRepository.GetAgendamentosAsync(null);
                    break;
            }

            var agendamentosViewModel = list.Select(x =>
                                                         new AgendamentosViewModel(x.Id, x.Dia, x.Cliente.Pessoa.Nome, x.HoraInicial, x.HoraFinal, (decimal)x.ValorAgendamento));

            return agendamentosViewModel;   
        }

        public async Task<IEnumerable<AgendamentosViewModel>> GetAgendamentos()
        {
            var list = await _agendamentoRepository.GetAgendamentosAsync(x => x.Status != EAgendamento.Pendente);
            var agendamentosViewModel = list.Select(x =>
                                                         new AgendamentosViewModel(x.Id, x.Dia, x.Cliente.Pessoa.Nome, x.HoraInicial, x.HoraFinal, (decimal)x.ValorAgendamento));

            return agendamentosViewModel;
        }

        public async Task<DetalhesAgendamentoViewModel> GetDetalheAgendamento(int id)
        {
            var agendamento = await _agendamentoRepository.GetAgendamentoPorId(id);
            var servicosAgendamento = await _agendamentoRepository.GetServicosPorAgendamentoId(id);
            var agendamentoViewModel = new DetalhesAgendamentoViewModel(agendamento.Id,
                                                                        agendamento.Nome,
                                                                        agendamento.Dia,
                                                                        agendamento.HoraInicial,
                                                                        agendamento.HoraFinal,
                                                                        agendamento.ValorProfissional,
                                                                        (decimal)agendamento.ValorAgendamento,
                                                                        agendamento.Cliente.Pessoa.Nome,
                                                                        agendamento.Cliente.TelefoneCelular,
                                                                        agendamento.Cliente.Whatsapp);
            
            var servicosAgendamentoViewModel = servicosAgendamento.Select(x => 
                                                                                           new ServicosAgendamentoViewModel(x.Servico.Nome, x.Servico.DuracaoEmMinutos, (decimal)x.Servico.ValorTotal));
            agendamentoViewModel.ServicosAgendamento = servicosAgendamentoViewModel;
            return agendamentoViewModel;
        }

        public async Task<IEnumerable<ServicosAgendamentoViewModel>> ListProdutosAgendamento(int id)
        {
            var servicosAgendamento = await _agendamentoRepository.GetServicosPorAgendamentoId(id);
            var servicosAgendamentoViewModel = servicosAgendamento.Select(x =>
                                                                               new ServicosAgendamentoViewModel(x.Servico.Nome, x.Servico.DuracaoEmMinutos, (decimal)x.Servico.ValorTotal));
            return servicosAgendamentoViewModel;
        }

        public async Task<CadastroAgendamentoInputModel> PrepararAgendamento()
        {
            var clientes = await _clienteRepository.GetClientesAsync(1, 9999999);
            var servicos = await _servicoRepository.GetServicosAsync();

            var clientesViewModel = clientes.Select(x =>
                                                        new ClienteVendaViewModel(x.Id, x.Pessoa.Nome));
            var servicosViewModel = servicos.Select(x =>
                                                        new ServicoListViewModel(x.Id, x.Nome, x.DuracaoEmMinutos, (decimal)x.ValorTotal));

            var inputModel = new CadastroAgendamentoInputModel()
            { 
                Clientes = clientesViewModel,
                Servicos = servicosViewModel
            };
            return inputModel;
        }

        public async Task<IEnumerable<RFrequenciaSalaoViewModel>> RFrequenciaSalao(FiltroRelatorioVAInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.FiltrarAgendamentosRelatorio(inputModel.ClienteId,
                                                                                          inputModel.Periodo,
                                                                                          inputModel.Inicial,
                                                                                          inputModel.Final);
            var dataPrimeiroRegistro = agendamentos.First().Dia;
            var dataUltimoRegistro = agendamentos.Last().Dia;
            TimeSpan diferenca = dataUltimoRegistro.Subtract(dataPrimeiroRegistro);
            int quantidadeDias = diferenca.Days;

            var list = new List<RFrequenciaSalaoViewModel>();
            // varios clientes
            if (inputModel.ClienteId == 0)
            {
                var clientesProcessados = new HashSet<int>();

                foreach (var agendamento in agendamentos)
                {
                    var id = agendamento.ClienteId;

                    if (clientesProcessados.Contains(id))
                    {
                        continue;
                    }

                    var agendamentosCliente = agendamentos.Where(x => x.ClienteId == id).ToList();

                    decimal valorTotal = agendamentosCliente.Sum(x => (decimal)x.ValorAgendamento);
                    var quantidade = agendamentosCliente.Count();
                    var frenciaMediaPeriodo = quantidadeDias / quantidade;
                    var nomeCliente = agendamentosCliente.First().Cliente.Pessoa.Nome;
                    decimal mediaTotal = valorTotal / quantidade;

                    var viewModel = new RFrequenciaSalaoViewModel(nomeCliente, frenciaMediaPeriodo, mediaTotal, valorTotal);
                    list.Add(viewModel);

                    clientesProcessados.Add(id);
                }
            }
            // apenas um cliente
            else
            {
                var quantidade = agendamentos.Count();
                if (quantidade != 0)
                {
                    decimal valorTotal = agendamentos.Sum(x => (decimal)x.ValorAgendamento);
                    var frenciaMediaPeriodo = quantidadeDias / quantidade;
                    var nomeCliente = agendamentos.First().Cliente.Pessoa.Nome;
                    decimal mediaTotal = valorTotal / quantidade;

                    var viewModel = new RFrequenciaSalaoViewModel(nomeCliente, frenciaMediaPeriodo, mediaTotal, valorTotal);
                    list.Add(viewModel);
                }

            }

            return list;
        }

        public async Task<IEnumerable<RPeriodoAgendamentosViewModel>> RPeriodoAgendamentos(FiltroRelatorioVAInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.FiltrarAgendamentosRelatorio(inputModel.ClienteId,
                                                                                         inputModel.Periodo,
                                                                                         inputModel.Inicial,
                                                                                         inputModel.Final);
            var list = agendamentos.Select(x =>
                                                new RPeriodoAgendamentosViewModel(x.Cliente.Pessoa.Nome,
                                                                                  x.Cliente.TelefoneCelular,
                                                                                  x.Cliente.Whatsapp,
                                                                                  x.Dia,
                                                                                  x.HoraInicial,
                                                                                  x.HoraFinal,
                                                                                  (decimal)x.ValorAgendamento));


            return list;
        }

        public async Task SalvarAgendamento(CadastroAgendamentoInputModel inputModel)
        {
            var agendamento = await _agendamentoRepository.GetAgendamentoPorId(inputModel.AgendamentoId);
            if (agendamento == null)
                throw new Exception("Agendamento não existe");

            agendamento.Atualizar(inputModel.Nome, inputModel.Dia, inputModel.HoraInicial, inputModel.HoraFinal, Decimal.Parse(inputModel.ValorProfissional), inputModel.ClienteId);
            agendamento.AdicionarValorAgendamento(Decimal.Parse(inputModel.ValorServicos));
            agendamento.AdicionarValorDesconto(Decimal.Parse(inputModel.ValorDesconto));

            await _agendamentoRepository.AtualizarAgendamentoAsync(agendamento);
        }

        public async Task<bool> VerificarDisponibilizadaAgendamento(CadastroAgendamentoInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentosAsync(x => x.Dia == inputModel.Dia);

            TimeSpan horaInicialInput = TimeSpan.Parse(inputModel.HoraInicial);
            TimeSpan horaFinalInput = TimeSpan.Parse(inputModel.HoraFinal);

            bool existeConflito = agendamentos.Any(agendamento =>
            {
                TimeSpan horaInicialAgendamento = TimeSpan.Parse(agendamento.HoraInicial);
                TimeSpan horaFinalAgendamento = TimeSpan.Parse(agendamento.HoraFinal);

                return (horaInicialInput >= horaInicialAgendamento && horaInicialInput < horaFinalAgendamento) ||
                       (horaFinalInput > horaInicialAgendamento && horaFinalInput <= horaFinalAgendamento) ||
                       (horaInicialInput <= horaInicialAgendamento && horaFinalInput >= horaFinalAgendamento);
            });

            return !existeConflito;
        }
    }
}
