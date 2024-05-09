namespace StudioHair.Application.ViewModels
{
    public class ResumoVendasViewModel
    {
        public ResumoVendasViewModel(decimal totalMensal, decimal totalDiario, decimal ticketMedio, int vendasHoje, DateTime ultimaVenda)
        {
            TotalMensal = totalMensal;
            TotalDiario = totalDiario;
            TicketMedio = ticketMedio;
            VendasHoje = vendasHoje;
            UltimaVenda = ultimaVenda;
        }

        public decimal TotalMensal { get; private set; }
        public decimal TotalDiario { get; private set; }
        public decimal TicketMedio { get; private set; }
        public int VendasHoje { get; private set; }
        public DateTime UltimaVenda { get; private set; }
    }
}
