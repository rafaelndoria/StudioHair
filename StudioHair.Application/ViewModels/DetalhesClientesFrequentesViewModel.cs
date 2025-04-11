namespace StudioHair.Application.ViewModels
{
    public class DetalhesClientesFrequentesViewModel
    {
        public DetalhesClientesFrequentesViewModel(string nomeCliente, int quantidadeVenda, int quantidadeAgendamentos, int clienteId)
        {
            NomeCliente = nomeCliente;
            QuantidadeVenda = quantidadeVenda;
            QuantidadeAgendamentos = quantidadeAgendamentos;
            ClienteId = clienteId;

            QuantidadeTotal = QuantidadeVenda + QuantidadeAgendamentos;
        }

        public string NomeCliente { get; private set; }
        public int QuantidadeVenda { get; private set; }
        public int QuantidadeAgendamentos { get; private set; }
        public int ClienteId { get; private set; }
        public int QuantidadeTotal { get; private set; }
    }
}
