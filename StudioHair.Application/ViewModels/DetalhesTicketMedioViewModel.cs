namespace StudioHair.Application.ViewModels
{
    public class DetalhesTicketMedioViewModel
    {
        public DetalhesTicketMedioViewModel(decimal valorVendas, decimal valorServicos, int quantidadeVendas, int quantidadeServios, string mesAno)
        {
            ValorVendas = valorVendas;
            ValorServicos = valorServicos;
            QuantidadeVendas = quantidadeVendas;
            QuantidadeServios = quantidadeServios;
            MesAno = mesAno;

            ValorTotal = ValorVendas + ValorServicos;
            QuantidadeTotal = QuantidadeVendas + QuantidadeServios;
            TicketMedioTotal = (ValorTotal / QuantidadeTotal) * 100;
            TicketMedioVendas = (ValorVendas / QuantidadeVendas) * 100;
            TicketMedioServicos = (ValorServicos / QuantidadeServios) * 100;
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
        public string MesAno { get; private set; }
    }
}
