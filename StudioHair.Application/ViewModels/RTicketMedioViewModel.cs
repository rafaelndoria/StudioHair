namespace StudioHair.Application.ViewModels
{
    public class RTicketMedioViewModel
    {
        public RTicketMedioViewModel(string nomeCliente, int comprasNoPeriodo, decimal totalCompras, decimal ticketMedio)
        {
            NomeCliente = nomeCliente;
            ComprasNoPeriodo = comprasNoPeriodo;
            TotalCompras = totalCompras;
            TicketMedio = ticketMedio;
        }

        public string NomeCliente { get; private set; }
        public int ComprasNoPeriodo { get; private set; }
        public decimal TotalCompras { get; private set; }
        public decimal TicketMedio { get; private set; }
    }
}
