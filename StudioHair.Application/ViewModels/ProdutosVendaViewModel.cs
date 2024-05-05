namespace StudioHair.Application.ViewModels
{
    public class ProdutosVendaViewModel
    {
        public ProdutosVendaViewModel(int produtoId, string nome, decimal valorUnitario, int quantidade, decimal valorTotal)
        {
            ProdutoId = produtoId;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
            ValorTotal = valorTotal;
            Nome = nome;
        }

        public int ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorTotal { get; private set; }
    }
}
