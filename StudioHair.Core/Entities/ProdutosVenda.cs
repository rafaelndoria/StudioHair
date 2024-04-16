namespace StudioHair.Core.Entities
{
    public class ProdutosVenda : Entidade
    {
        public ProdutosVenda( decimal valor, int vendaId, int produtoId)
        {
            Valor = valor;
            ProdutoId = produtoId;
            VendaId = vendaId;
        }

        public decimal Valor { get; private set; }
        public int ProdutoId { get; private set; }
        public int VendaId { get; private set; }

        public Produto? Produto { get; set; }
        public Venda? Venda { get; set; }
    }
}
