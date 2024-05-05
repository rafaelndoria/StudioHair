namespace StudioHair.Application.ViewModels
{
    public class DetalhesVendaViewModel
    {
        public DetalhesVendaViewModel(int id, string nomeCliente, DateTime dataVenda, string tipoPagamento, decimal valorDesconto, decimal total)
        {
            Id = id;
            NomeCliente = nomeCliente;
            DataVenda = dataVenda;
            TipoPagamento = tipoPagamento;
            ValorDesconto = valorDesconto;
            Total = total;
        }

        public int Id { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataVenda { get; private set; }
        public string TipoPagamento { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal Total { get; private set; }
        public IEnumerable<ProdutosVendaViewModel> ProdutosVenda { get; set; } = new List<ProdutosVendaViewModel>();
    }
}
