using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;
using System.Security.AccessControl;

namespace StudioHair.Application.Services.Implementations
{
    public class AnaliseGerencialService : IAnaliseGerencialService
    {
        private readonly IProdutoService _produtoService;
        private readonly IClienteService _clienteService;
        private readonly IServicoService _servicoService;
        private readonly IVendaService _vendaService;
        private readonly IAgendamentoService _agendamentoService;
        private readonly IVendaRepository _vendaRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AnaliseGerencialService(IProdutoService produtoService, IClienteService clienteService, IServicoService servicoService, IVendaService vendaService, IAgendamentoService agendamentoService, IVendaRepository vendaRepository, IAgendamentoRepository agendamentoRepository)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;
            _servicoService = servicoService;
            _vendaService = vendaService;
            _agendamentoService = agendamentoService;
            _vendaRepository = vendaRepository;
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<DadosClientesFrequentesViewModel> BuscarClientesFrequentes(FiltroAnaliseInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentoAnalise(inputModel.ServicoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);
            var vendas = await _vendaRepository.GetVendasAnalise(inputModel.ProdutoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);

            var vendasAgrupadasCliente = vendas
                .GroupBy(x => new
                {
                    x.ClienteId
                })
                .Select(g => new
                {
                    NomeCliente = g.First().Cliente.Pessoa.Nome,
                    Quantidade = g.Count(),
                    Tipo = "Vendas",
                    Datas = g.Select(x => x.DataDaVenda).ToList(),
                    ClienteId = g.Key.ClienteId
                });
            var agendamentosAgrupadosCliente = agendamentos
                .GroupBy(x => new
                {
                    x.ClienteId
                })
                .Select(g => new
                {
                    NomeCliente = g.First().Cliente.Pessoa.Nome,
                    Quantidade = g.Count(),
                    Tipo = "Agendamento",
                    Datas = g.Select(x => x.Dia).ToList(),
                    ClienteId = g.Key.ClienteId
                });
            var listaAgrupada = vendasAgrupadasCliente
                .Concat(agendamentosAgrupadosCliente)
                .GroupBy(x => new { x.ClienteId, x.Tipo })
                .Select(g => new
                {
                    NomeCliente = g.First().NomeCliente,
                    Tipo = g.First().Tipo,
                    QuantidadeTotal = g.First().Quantidade,
                    Datas = g.First().Datas,
                    ClienteId = g.First().ClienteId
                });

            var clientesFrequentesViewModel = new List<DetalhesClientesFrequentesViewModel>();
            var clientesLidos = new List<int>();
            foreach (var registro in listaAgrupada)
            {
                if (!clientesLidos.Contains(registro.ClienteId))
                {
                    var quantidadeVenda = 0;
                    var quantidadeAgendamento = 0;
                    var nomeCliente = registro.NomeCliente;
                    foreach (var item in listaAgrupada.Where(x => x.ClienteId == registro.ClienteId))
                    {
                        if (item.Tipo == "Agendamento")
                        {
                            quantidadeAgendamento += item.QuantidadeTotal;
                        }
                        else
                        {
                            quantidadeVenda += item.QuantidadeTotal;
                        }
                    }
                    clientesFrequentesViewModel.Add(new DetalhesClientesFrequentesViewModel(nomeCliente, quantidadeVenda, quantidadeAgendamento, registro.ClienteId));
                    clientesLidos.Add(registro.ClienteId);
                }
            }

            var clienteComMaiorQuantidade = clientesFrequentesViewModel
                .MaxBy(x => x.QuantidadeTotal);
            var datasCliente = listaAgrupada.Where(x => x.ClienteId == clienteComMaiorQuantidade.ClienteId).SelectMany(x => x.Datas).ToList();

            return new DadosClientesFrequentesViewModel(clienteComMaiorQuantidade.NomeCliente,
                                                        clienteComMaiorQuantidade.QuantidadeVenda,
                                                        clienteComMaiorQuantidade.QuantidadeAgendamentos,
                                                        datasCliente,
                                                        clientesFrequentesViewModel);
        }

        public async Task<DadosAnaliseViewModel> BuscarDadosAnalise(FiltroAnaliseInputModel inputModel)
        {
            DadosAnaliseViewModel dadosViewModel = new DadosAnaliseViewModel();

            switch (inputModel.TipoAnalise)
            {
                case ETipoAnaliseGerencial.FaturamentoMensal:
                    dadosViewModel.FaturamentoMensal = await BuscarFaturamentoMensal(inputModel);
                    dadosViewModel.NomeViewAnalise = "FaturamentoMensal";
                    break;
                case ETipoAnaliseGerencial.ServicosMaisVendidos:
                    dadosViewModel.ServicosMaisProcurados = await BuscarServicosMaisProcurados(inputModel);
                    dadosViewModel.NomeViewAnalise = "ServicosMaisVendidos";
                    break;
                case ETipoAnaliseGerencial.ProdutosMaisVendidos:
                    dadosViewModel.ProdutosMaisVendidos = await BuscarProdutosMaisVendidos(inputModel);
                    dadosViewModel.NomeViewAnalise = "ProdutosMaisVendidos";
                    break;
                case ETipoAnaliseGerencial.TicketMedioCliente:
                    dadosViewModel.TicketMedio = await BuscarTicketMedio(inputModel);
                    dadosViewModel.NomeViewAnalise = "TicketMedio";
                    break;
                case ETipoAnaliseGerencial.HorarioDePico:
                    dadosViewModel.HorarioPico = await BuscarHorarioPico(inputModel);
                    dadosViewModel.NomeViewAnalise = "HorarioPico";
                    break;
                case ETipoAnaliseGerencial.ClientesFrequentes:
                    dadosViewModel.ClientesFrequentes = await BuscarClientesFrequentes(inputModel);
                    dadosViewModel.NomeViewAnalise = "ClientesFrequentes";
                    break;
            }

            return dadosViewModel;
        }

        public async Task<DadosFaturamentoMensalViewModel> BuscarFaturamentoMensal(FiltroAnaliseInputModel inputModel)
        {
            var vendas = await _vendaRepository.GetVendasAnalise(
                inputModel.ProdutoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);

            var agendamentos = await _agendamentoRepository.GetAgendamentoAnalise(
                inputModel.ServicoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);

            var vendasMesDetalhe = vendas
                .GroupBy(v => new { v.DataDaVenda.Year, v.DataDaVenda.Month })
                .Select(s => new
                {
                    ValorTotal = s.Sum(v => v.Total),
                    QuantidadeTotal = s.Count(),
                    Ano = s.Key.Year,
                    Mes = s.Key.Month,
                    Origem = "Vendas"
                });

            var agendamentosMesDetalhe = agendamentos
                .GroupBy(v => new { v.Dia.Year, v.Dia.Month })
                .Select(s => new
                {
                    ValorTotal = s.Sum(v => v.ValorAgendamento),
                    QuantidadeTotal = s.Count(),
                    Ano = s.Key.Year,
                    Mes = s.Key.Month,
                    Origem = "Agendamentos"
                });

            var faturamentosDetalhe = vendasMesDetalhe.Concat(agendamentosMesDetalhe);

            var faturamentoMesDetalhe = faturamentosDetalhe
                .GroupBy(x => new { x.Ano, x.Mes })
                .OrderBy(g => new DateTime(g.Key.Ano, g.Key.Mes, 1))
                .Select(g =>
                {
                    var totalVendas = g.Where(x => x.Origem == "Vendas").Sum(x => x.ValorTotal);
                    var totalServicos = g.Where(x => x.Origem == "Agendamentos").Sum(x => x.ValorTotal);
                    var quantidadeVendas = g.Where(x => x.Origem == "Vendas").Sum(x => x.QuantidadeTotal);
                    var quantidadeServicos = g.Where(x => x.Origem == "Agendamentos").Sum(x => x.QuantidadeTotal);

                    var mesAno = $"{g.Key.Mes:D2}/{g.Key.Ano}";

                    return new FaturamentoMensalDetalhesMesViewModel(
                        (decimal)totalVendas,
                        (decimal)totalServicos,
                        quantidadeVendas,
                        quantidadeServicos,
                        mesAno
                    );
                })
                .ToList();

            return new DadosFaturamentoMensalViewModel(
                (decimal)vendas.Sum(x => x.Total),
                (decimal)agendamentos.Sum(x => x.ValorAgendamento),
                vendas.Count(),
                agendamentos.Count(),
                faturamentoMesDetalhe
            );
        }

        public async Task<DadosHorarioPicoViewModel> BuscarHorarioPico(FiltroAnaliseInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentoAnalise(inputModel.ServicoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);
            var vendas = await _vendaRepository.GetVendasAnalise(inputModel.ProdutoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);

            var vendasAgrupadas = vendas
                .GroupBy(x => new { x.DataDaVenda.Day, x.DataDaVenda.Month, x.DataDaVenda.Year })
                .Select(g => new
                {
                    Tipo = "Venda",
                    Data = g.Key,
                    HorariosIniciais = g.Select(x => x.DataDaVenda.ToString("HH:mm")).ToList()
                });
            var agendamentosAgrupadas = agendamentos
                .GroupBy(x => new { x.Dia.Day, x.Dia.Month, x.Dia.Year })
                .Select(g => new
                {
                    Tipo = "Agendamento",
                    Data = g.Key,
                    HorariosIniciais = g.Select(x => x.HoraInicial).ToList()
                });
            var listaCompleta = vendasAgrupadas
                .Concat(agendamentosAgrupadas)
                .GroupBy(x => x.Data)
                .Select(g => new
                {
                    Data = g.Key,
                    Tipos = g.Select(x => x.Tipo).Distinct().ToList(),
                    HorariosIniciais = g.SelectMany(x => x.HorariosIniciais).ToList()
                })
                .ToList();

            var dadosHorarioViewModel = new List<DetalhesHorarioPicoViewModel>();
            foreach (var registro in listaCompleta)
            {
                var agrupamentoPorHora = registro.HorariosIniciais
                    .GroupBy(h => h.Substring(0, 2))
                    .Select(g => new
                    {
                        Hora = g.Key + ":00",
                        Quantidade = g.Count(),

                    });

                var dadosHoraViewModel = new List<DetalhesHoraHorarioPicoViewModel>();
                foreach (var horario in agrupamentoPorHora)
                {
                    var dadosHora = new DetalhesHoraHorarioPicoViewModel(horario.Hora, horario.Quantidade);
                    dadosHoraViewModel.Add(dadosHora);
                }

                var horarioPico = new DetalhesHorarioPicoViewModel($"{registro.Data.Day}/{registro.Data.Month}/{registro.Data.Year}", dadosHoraViewModel);
                dadosHorarioViewModel.Add(horarioPico);
            }

            return new DadosHorarioPicoViewModel(dadosHorarioViewModel);
        }

        public async Task<DadosProdutosMaisVendidosViewModel> BuscarProdutosMaisVendidos(FiltroAnaliseInputModel inputModel)
        {
            var vendas = await _vendaRepository.GetVendasAnalise(inputModel.ProdutoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);
            var produtoMaisVendido = vendas
                .SelectMany(a => a.ProdutosVendas)
                .GroupBy(s => s.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First())
                .FirstOrDefault();

            var nomeProdutoMaisVendido = produtoMaisVendido.Produto.Nome;
            decimal quantidadeVendaProduto = 0;
            var quantidadeTotalProduto = 0;
            var datas = new List<DateTime>();

            foreach (var item in vendas.Where(x => x.ProdutosVendas.Any(x => x.ProdutoId == produtoMaisVendido.ProdutoId)))
            {
                quantidadeVendaProduto += item.ProdutosVendas.Where(x => x.ProdutoId == produtoMaisVendido.ProdutoId).Sum(x => x.ValorTotal);
                quantidadeTotalProduto += item.ProdutosVendas.Where(x => x.ProdutoId == produtoMaisVendido.ProdutoId).Count();
                datas.Add(item.DataDaVenda);
            }

            var produtosVendaDetalhe = vendas
                .SelectMany(x => x.ProdutosVendas)
                .GroupBy(x => x.Produto.Id)
                .Select(x =>
                {
                    return new DetalhesDadosProdutosMaisProcuradosViewModel(x.First().Produto.Nome,
                                                                            x.Count(),
                                                                            x.Sum(x => x.ValorTotal));
                });

            return new DadosProdutosMaisVendidosViewModel(nomeProdutoMaisVendido, quantidadeTotalProduto, quantidadeVendaProduto, datas, produtosVendaDetalhe);
        }

        public async Task<DadosServicosMaisProcuradosViewModel> BuscarServicosMaisProcurados(FiltroAnaliseInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentoAnalise(inputModel.ServicoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);
            var servicoMaisFrequente = agendamentos
                .SelectMany(a => a.AgendamentoServicos)
                .GroupBy(s => s.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First())
                .FirstOrDefault();

            var nomeServicoMaisFrequente = servicoMaisFrequente.Servico.Nome;
            decimal totalServicosFrequente = 0;
            var totalServicos = 0;
            var datas = new List<DateTime>();

            foreach (var item in agendamentos.Where(x => x.AgendamentoServicos.Any(x => x.Servico.Id == servicoMaisFrequente.Servico.Id)))
            {
                totalServicosFrequente += (decimal)item.ValorAgendamento;
                totalServicos += item.AgendamentoServicos.Count();
                datas.Add(item.Dia);
            }

            var detalhesServicoAgendamento = agendamentos
                .SelectMany(x => x.AgendamentoServicos)
                .GroupBy(x => x.Servico.Id)
                .Select(s =>
                {
                    return new DetalhesDadosServicosMaisProcuradosViewModel(s.First().Servico.Nome,
                                                                            s.Count(),
                                                                            (decimal)s.Sum(x => x.Servico.ValorTotal * s.Count()));
                });

            return new DadosServicosMaisProcuradosViewModel(nomeServicoMaisFrequente, totalServicos, totalServicosFrequente, datas, detalhesServicoAgendamento);
        }

        public async Task<DadosTicketMedioViewModel> BuscarTicketMedio(FiltroAnaliseInputModel inputModel)
        {
            var agendamentos = await _agendamentoRepository.GetAgendamentoAnalise(inputModel.ServicoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);
            var vendas = await _vendaRepository.GetVendasAnalise(inputModel.ProdutoId, inputModel.ClienteId, inputModel.TipoPeriodo, inputModel.DataInicial, inputModel.DataFinal);

            return new DadosTicketMedioViewModel((decimal)vendas.Sum(x => x.Total), (decimal)agendamentos.Sum(x => x.ValorAgendamento), vendas.Count(), agendamentos.Count());
        }

        public async Task<FiltroAnaliseInputModel> ObterFiltroAnalises(ETipoAnaliseGerencial tipoAnaliseGerencial)
        {
            var filtroAnalisesViewModel = new FiltroAnaliseInputModel();
            filtroAnalisesViewModel.TipoAnalise = tipoAnaliseGerencial;
            try
            {
                switch (tipoAnaliseGerencial)
                {
                    case ETipoAnaliseGerencial.FaturamentoMensal:
                    case ETipoAnaliseGerencial.TicketMedioCliente:
                    case ETipoAnaliseGerencial.ClientesFrequentes:
                        filtroAnalisesViewModel.Produtos = await _produtoService.GetProdutos();
                        filtroAnalisesViewModel.Servicos = await _servicoService.GetServicos();
                        filtroAnalisesViewModel.Clientes = await _clienteService.GetClientes();
                        break;
                    case ETipoAnaliseGerencial.ServicosMaisVendidos:
                        filtroAnalisesViewModel.Servicos = await _servicoService.GetServicos();
                        filtroAnalisesViewModel.Clientes = await _clienteService.GetClientes();
                        break;
                    case ETipoAnaliseGerencial.ProdutosMaisVendidos:
                        filtroAnalisesViewModel.Produtos = await _produtoService.GetProdutos();
                        filtroAnalisesViewModel.Clientes = await _clienteService.GetClientes();
                        break;
                    default:
                        break;

                }
                return filtroAnalisesViewModel;
            }
            catch
            {
                return filtroAnalisesViewModel;
            }
        }
    }
}
