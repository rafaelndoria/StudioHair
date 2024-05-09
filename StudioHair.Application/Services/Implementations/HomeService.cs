using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudioHair.Application.Services.Implementations
{
    public class HomeService : IHomeService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IServicoRepository _servicoRepository;

        private readonly int diaAtual = DateTime.Now.Day;
        private readonly int mesAtual = DateTime.Now.Month;
        private readonly int anoAtual = DateTime.Now.Year;
        private readonly int horaAtual = DateTime.Now.Hour;

        public HomeService(IVendaRepository vendaRepository, IAgendamentoRepository agendamentoRepository, IProdutoRepository produtoRepository, IServicoRepository servicoRepository)
        {
            _vendaRepository = vendaRepository;
            _agendamentoRepository = agendamentoRepository;
            _produtoRepository = produtoRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<ResumoVendasViewModel> PrapararResumoVendas()
        {
            var vendas = await _vendaRepository.GetVendasAsync();

            decimal totalMes = 0;
            decimal totalDia = 0;
            DateTime ultimaVenda = DateTime.Now;
            int vendasHoje = 0;
            decimal mediaTotal = 0;

            var primeiroRegistro = true;
            var quantidade = 0;
            decimal valorTotal = 0;
            foreach (var venda in vendas.Where(x => x.DataDaVenda.Year == anoAtual || x.DataDaVenda.Month == mesAtual).OrderByDescending(x => x.DataDaVenda))
            {
                if (primeiroRegistro)
                {
                    ultimaVenda = venda.DataDaVenda;
                    primeiroRegistro = false;
                }
                if (venda.DataDaVenda.Day == diaAtual)
                {
                    totalDia += (decimal)venda.Total;
                    vendasHoje++;
                }
                else
                {
                    totalMes += (decimal)venda.Total;
                }
                valorTotal += (decimal)venda.Total;
                quantidade++;
            }
            mediaTotal = valorTotal / quantidade;

            mediaTotal = Math.Round(mediaTotal, 2);

            var resumoVendasViewModel = new ResumoVendasViewModel(totalMes, totalDia, mediaTotal, vendasHoje, ultimaVenda);

            return resumoVendasViewModel;
        }

        public async Task<ResumoHomeViewModel> PrepararResumo()
        {
            var resumoVendas = await PrapararResumoVendas();
            var resumoAgendamentos = await PrepararResumoAgendamentos();
            var resumoProdutos = await PrepararResumoProdutos();
            var resumoServicos = await PrepararResumoServicos();

            var resumoHomeViewModel = new ResumoHomeViewModel(resumoVendas, resumoAgendamentos, resumoProdutos, resumoServicos);

            return resumoHomeViewModel;
        }

        public async Task<ResumoAgendamentosViewModel> PrepararResumoAgendamentos()
        {
            var quantidadeAgendamentosDiario = 0;
            var quantidadeProximosAgendamentos = 0;
            var proximoAgendamento = "";
            decimal totalAgendamentoDiario = 0;
            decimal totalAgendamentoMensal = 0;

            var agendamentos = await _agendamentoRepository.GetAgendamentosAsync(x => x.Dia.Month >= mesAtual || x.Dia.Year >= anoAtual);

            foreach (var agendamento in agendamentos)
            {
                if (agendamento.Dia.Day == diaAtual)
                {
                    totalAgendamentoDiario += (decimal)agendamento.ValorAgendamento;
                    quantidadeAgendamentosDiario++;
                }
                else if (agendamento.Dia.Month == mesAtual || agendamento.Dia.Year == anoAtual)
                {
                    totalAgendamentoMensal += (decimal)agendamento.ValorAgendamento;
                }
                quantidadeProximosAgendamentos++;
            }

            var agendamentoFiltrado = agendamentos.OrderByDescending(x => x.Dia).FirstOrDefault();
            if (agendamentoFiltrado == null)
            {
                proximoAgendamento = null;
            }
            else
            {
                proximoAgendamento = $"Dia: {agendamentoFiltrado.Dia.ToString("dd/MM/yyyy")}, Inicio: {agendamentoFiltrado.HoraInicial} / Final: {agendamentoFiltrado.HoraFinal}";
            }

            var resumoAgendamentoViewModel = new ResumoAgendamentosViewModel(quantidadeAgendamentosDiario, quantidadeProximosAgendamentos, proximoAgendamento, totalAgendamentoDiario, totalAgendamentoMensal);

            return resumoAgendamentoViewModel;
        }

        public async Task<ResumoProdutosViewModel> PrepararResumoProdutos()
        {
            var produtos = await _produtoRepository.GetProdutosAsync();

            var quantidadeProdutos = produtos.Count();
            var produtosVenda = produtos.Where(x => x.ProdutoParaVenda == true).Count();
            var produtosControlaEstoque = produtos.Where(x => x.ControlaEstoque == true).Count();

            var resumoProdutosViewModel = new ResumoProdutosViewModel(quantidadeProdutos, produtosControlaEstoque, produtosVenda);

            return resumoProdutosViewModel;
        }

        public async Task<ResumoServicosViewModel> PrepararResumoServicos()
        {
            var servicos = await _servicoRepository.GetServicosAsync();

            var servicosCriados = servicos.Count();

            var resumoServicosViewModel = new ResumoServicosViewModel(servicosCriados);

            return resumoServicosViewModel;
        }
    }
}