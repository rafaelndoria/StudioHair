using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace StudioHair.Application.Services.Implementations
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IServicoRepository servicoRepository, IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _servicoRepository = servicoRepository;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task AdicionarServicoAgendamento(CadastroAgendamentoInputModel inputModel)
        {
            var servicoAgendamento = new AgendamentoServicos(inputModel.AgendamentoId, inputModel.ServicoId);
            await _agendamentoRepository.AdicionarServicoAgendamento(servicoAgendamento);
        }

        public async Task ConfirmarAgendamento(int agendamentoId)
        {
            var agendamento = await _agendamentoRepository.GetAgendamentoPorId(agendamentoId);
            if (agendamento == null)
                throw new Exception("Agendamento não encontrado.");

            agendamento.AlterarStatus(EAgendamento.Confirmado);
            await _agendamentoRepository.AtualizarAgendamentoAsync(agendamento);
        }

        public async Task<int> CriarAgendamentoCliente(CriarAgendamentoClienteInputModel inputModel)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(inputModel.UsuarioId);
            var agendamento = new Agendamento("Agendamento Criado Pelo Cliente", inputModel.Dia, inputModel.HoraInicial, "", 0, usuario.Pessoa.Cliente.Id);
            var agendamentoId = await _agendamentoRepository.CriarAgendamento(agendamento);

            var quantidadeMinutos = 0;

            foreach (var servicoId in inputModel.ServicosAdicionados)
            {
                var servico = await _servicoRepository.GetServicoPorIdAsync(servicoId);
                quantidadeMinutos += servico.DuracaoEmMinutos;
                var agendamentoServico = new AgendamentoServicos(agendamentoId, servicoId);
                agendamento.AdicionarValorAgendamento((decimal)servico.ValorTotal);
                await _agendamentoRepository.AdicionarServicoAgendamento(agendamentoServico);
            }

            TimeSpan horaInicialTimeSpan = TimeSpan.Parse(inputModel.HoraInicial);
            TimeSpan duracaoTotal = TimeSpan.FromMinutes(quantidadeMinutos);
            TimeSpan horaFinalTimeSpan = horaInicialTimeSpan.Add(duracaoTotal);
            string horaFinalString = horaFinalTimeSpan.ToString(@"hh\:mm");
            agendamento.Atualizar("Agendamento Criado Pelo Cliente", inputModel.Dia, inputModel.HoraInicial, horaFinalString, 0, usuario.Pessoa.Cliente.Id);
            agendamento.AlterarStatus(EAgendamento.Pendente);
            await _agendamentoRepository.AtualizarAgendamentoAsync(agendamento);

            return agendamentoId;
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

        public async Task<AgendamentoClienteInputModel> GetHorariosDisponiveis(DateTime dataAgendamento)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentosAsync(x => x.Dia == dataAgendamento.Date);
            var horariosViewModel = VerificarHorarios(agendamentos);
            var inputModel = new AgendamentoClienteInputModel()
            { 
                Horarios = horariosViewModel,
                Dia = dataAgendamento.Date
            };

            return inputModel;
        }

        public async Task<ResumoAgendamentoViewModel> GetResumoAgendamento(int agendamentoId)
        {
            var agendamento = await _agendamentoRepository.GetAgendamentoPorId(agendamentoId);
            var resumoAgendamento = new ResumoAgendamentoViewModel(agendamento.Dia, agendamento.HoraInicial, agendamento.HoraFinal, (decimal)agendamento.ValorAgendamento, agendamentoId);

            foreach (var item in agendamento.AgendamentoServicos)
            {
                var servicoAdicionado = new ServicosAgendamentoViewModel(item.Servico.Nome, item.Servico.DuracaoEmMinutos, item.Servico.ValorServico);
                resumoAgendamento.Servicos.Add(servicoAdicionado);
            }

            return resumoAgendamento;
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
                    var nomeCliente = agendamentosCliente.First().Cliente.Pessoa.Nome;
                    decimal mediaTotal = valorTotal / quantidade;

                    var viewModel = new RFrequenciaSalaoViewModel(nomeCliente, quantidade, mediaTotal, valorTotal);
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
                    var nomeCliente = agendamentos.First().Cliente.Pessoa.Nome;
                    decimal mediaTotal = valorTotal / quantidade;

                    var viewModel = new RFrequenciaSalaoViewModel(nomeCliente, quantidade, mediaTotal, valorTotal);
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
            var cultureInfo = new CultureInfo("pt-BR");

            var agendamento = await _agendamentoRepository.GetAgendamentoPorId(inputModel.AgendamentoId);
            if (agendamento == null)
                throw new Exception("Agendamento não existe");

            // Substitui ',' por '.' nas strings antes de fazer as conversões
            string valorProfissionalString = inputModel.ValorProfissional.Replace("R$", "").Replace(".", ",").Trim();
            string valorServicosString = inputModel.ValorServicos.Replace(".", ",");
            string valorDescontoString = inputModel.ValorDesconto.Replace("R$", "").Replace(".", ",").Trim();

            // Converte as strings para decimal
            decimal valorProfissional = decimal.Parse(valorProfissionalString, cultureInfo);
            decimal valorServicos = decimal.Parse(valorServicosString, cultureInfo);
            decimal valorDesconto = decimal.Parse(valorDescontoString, cultureInfo);

            agendamento.Atualizar(inputModel.Nome, inputModel.Dia, inputModel.HoraInicial, inputModel.HoraFinal, valorProfissional, inputModel.ClienteId);
            agendamento.AdicionarValorAgendamento(valorServicos);
            agendamento.AdicionarValorDesconto(valorDesconto);

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

        public IEnumerable<HorariosDisponiveisViewModel> VerificarHorarios(List<Agendamento> agendamentos)
        {
            var horarios = new List<HorariosDisponiveisViewModel>();

            if (agendamentos.Count == 0)
            {
                horarios.Add(new HorariosDisponiveisViewModel("00:00", "23:59"));
                return horarios;
            }

            // Converte os horários para TimeSpan e ordena os agendamentos
            agendamentos = agendamentos
                .OrderBy(a => TimeSpan.Parse(a.HoraInicial))
                .ToList();

            // Primeiro horário livre do dia até o primeiro agendamento
            var inicioDia = TimeSpan.FromHours(0);
            var primeiroHorarioInicial = TimeSpan.Parse(agendamentos[0].HoraInicial);

            if (agendamentos.Count > 0 && primeiroHorarioInicial > inicioDia)
            {
                horarios.Add(new HorariosDisponiveisViewModel(
                    "00:00",
                    primeiroHorarioInicial.Add(-TimeSpan.FromMinutes(1)).ToString(@"hh\:mm")
                ));
            }

            // Verifica os intervalos entre os agendamentos
            for (int i = 0; i < agendamentos.Count - 1; i++)
            {
                var terminoAtual = TimeSpan.Parse(agendamentos[i].HoraFinal);
                var inicioProximo = TimeSpan.Parse(agendamentos[i + 1].HoraInicial);

                if (terminoAtual < inicioProximo)
                {
                    horarios.Add(new HorariosDisponiveisViewModel(
                        terminoAtual.Add(TimeSpan.FromMinutes(1)).ToString(@"hh\:mm"),
                        inicioProximo.Add(-TimeSpan.FromMinutes(1)).ToString(@"hh\:mm")
                    ));
                }
            }

            // Último horário livre do último agendamento até o final do dia
            var fimDia = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
            var ultimoTermino = TimeSpan.Parse(agendamentos.Last().HoraFinal);

            if (ultimoTermino < fimDia)
            {
                horarios.Add(new HorariosDisponiveisViewModel(
                    ultimoTermino.Add(TimeSpan.FromMinutes(1)).ToString(@"hh\:mm"),
                    "23:59"
                ));
            }

            return horarios;
        }
    }
}
