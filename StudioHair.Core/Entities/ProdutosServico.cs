namespace StudioHair.Core.Entities
{
    public class ProdutosServico : Entidade
    {
        public ProdutosServico(int quantidadeUtilizada, decimal valorProduto, int produtoId, int servicoId)
        {
            QuantidadeUtilizada = quantidadeUtilizada;
            ValorProduto = valorProduto;
            ProdutoId = produtoId;
            ServicoId = servicoId;
        }

        public int QuantidadeUtilizada { get; private set; }
        public decimal ValorProduto { get; private set; }
        public int ProdutoId { get; private set; }
        public int ServicoId { get; private set; }
    }
}
