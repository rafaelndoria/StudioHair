namespace StudioHair.Application.ViewModels
{
    public class DadosAnaliseViewModel
    {
        public string NomeViewAnalise { get; set; }
        public DadosFaturamentoMensalViewModel FaturamentoMensal {  get; set; }
        public DadosServicosMaisProcuradosViewModel ServicosMaisProcurados { get; set; }
        public DadosProdutosMaisVendidosViewModel ProdutosMaisVendidos { get; set; }
        public DadosTicketMedioViewModel TicketMedio { get; set; }
        public DadosHorarioPicoViewModel HorarioPico { get; set; }
        public DadosClientesFrequentesViewModel ClientesFrequentes { get; set; }
    }
}
