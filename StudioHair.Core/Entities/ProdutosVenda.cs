namespace StudioHair.Core.Entities
{
    public class ProdutosVenda : Entidade
    {
        public ProdutosVenda(int produtoId, decimal valor, int vendaId)
        {
            ProdutoId = produtoId;
            Valor = valor;
            VendaId = vendaId;
        }

        public int ProdutoId { get; private set; }
        public decimal Valor { get; private set; }
        public int VendaId { get; private set; }
    }
}
