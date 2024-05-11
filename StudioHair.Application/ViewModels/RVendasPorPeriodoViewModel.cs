namespace StudioHair.Application.ViewModels
{
    public class RVendasPorPeriodoViewModel
    {
        public RVendasPorPeriodoViewModel(DateTime dataVenda, string cliente, string tipoPagamento, decimal total)
        {
            DataVenda = dataVenda;
            Cliente = cliente;
            TipoPagamento = tipoPagamento;
            Total = total;
        }

        public DateTime DataVenda { get; private set; }
        public string Cliente { get; private set; }
        public string TipoPagamento { get; private set; }
        public decimal Total { get; private set; }
    }
}
