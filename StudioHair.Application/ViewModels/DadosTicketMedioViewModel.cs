namespace StudioHair.Application.ViewModels
{
    public class DadosTicketMedioViewModel
    {
        public DadosTicketMedioViewModel(decimal valorVendas, decimal valorServicos, int quantidadeVendas, int quantidadeServios)
        {
            ValorVendas = valorVendas;
            ValorServicos = valorServicos;
            QuantidadeVendas = quantidadeVendas;
            QuantidadeServios = quantidadeServios;

            ValorTotal = ValorVendas + ValorServicos;
            QuantidadeTotal = QuantidadeVendas + QuantidadeServios;
            TicketMedioTotal = ValorTotal / QuantidadeTotal;
            TicketMedioVendas = ValorVendas / QuantidadeVendas;
            TicketMedioServicos = ValorServicos / QuantidadeServios;
        }

        public decimal ValorVendas { get; private set; }
        public decimal ValorServicos { get; private set; }
        public int QuantidadeVendas { get; private set; }
        public int QuantidadeServios { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public decimal TicketMedioTotal { get; private set; }
        public decimal TicketMedioVendas { get; private set; }
        public decimal TicketMedioServicos { get; private set; }
    }
}
