namespace StudioHair.Application.ViewModels
{
    public class DadosClientesFrequentesViewModel
    {
        public DadosClientesFrequentesViewModel(string nomeCliente, int quantidadeVenda, int quantidadeAgendamentos, List<DateTime> dias, IEnumerable<DetalhesClientesFrequentesViewModel> detalhesClientesFrequentes)
        {
            NomeCliente = nomeCliente;
            QuantidadeVenda = quantidadeVenda;
            QuantidadeAgendamentos = quantidadeAgendamentos;
            Dias = dias;
            DetalhesClientesFrequentes = detalhesClientesFrequentes;

            QuantidadeTotal = QuantidadeVenda + QuantidadeAgendamentos;
        }

        public string NomeCliente { get; private set; }
        public int QuantidadeVenda { get; private set; }
        public int QuantidadeAgendamentos { get; private set; }
        public List<DateTime> Dias { get; private set; }
        public IEnumerable<DetalhesClientesFrequentesViewModel> DetalhesClientesFrequentes { get; private set; }
        public int QuantidadeTotal { get; private set; }
    }
}
