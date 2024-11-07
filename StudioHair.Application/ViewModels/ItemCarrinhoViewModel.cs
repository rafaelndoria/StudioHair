namespace StudioHair.Application.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public ItemCarrinhoViewModel(string nomeProduto, decimal quantidade, decimal valorTotal, decimal valorUnitario, int produtoId)
        {
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorTotal = valorTotal;
            ValorUnitario = valorUnitario;
            ProdutoId = produtoId;
        }

        public int ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal ValorUnitario { get; private set; }
    }
}
