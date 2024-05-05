namespace StudioHair.Core.Entities
{
    public class ProdutosVenda : Entidade
    {
        public ProdutosVenda()
        {
            
        }
        public ProdutosVenda(decimal valor, int quantidade, int vendaId, int produtoId)
        {
            ValorUnitario = valor;
            Quantidade = quantidade;
            ProdutoId = produtoId;
            VendaId = vendaId;

            ValorTotal = ValorUnitario * Quantidade;
        }

        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int ProdutoId { get; private set; }
        public int VendaId { get; private set; }

        public Produto? Produto { get; set; }
        public Venda? Venda { get; set; }
    }
}
