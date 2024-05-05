namespace StudioHair.Application.ViewModels
{
    public class VendasViewModel
    {
        public VendasViewModel(int id, string nomeCliente, DateTime dataVenda, decimal valorTotal, string formaDePagamento)
        {
            Id = id;
            NomeCliente = nomeCliente;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            FormaDePagamento = formaDePagamento;
        }

        public int Id { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataVenda { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string FormaDePagamento { get; private set; }
    }
}
