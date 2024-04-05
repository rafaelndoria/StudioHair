namespace StudioHair.Core.Entities
{
    public class ProdutosServico : Entidade
    {
        public ProdutosServico(int produtoId, int servicoId, int quantidadeUtilizada, decimal valorProdutoCalculado)
        {
            ProdutoId = produtoId;
            ServicoId = servicoId;
            QuantidadeUtilizada = quantidadeUtilizada;
            ValorProdutoCalculado = valorProdutoCalculado;
        }

        public int ProdutoId { get; private set; }
        public int ServicoId { get; private set; }
        public int QuantidadeUtilizada { get; private set; }
        public decimal ValorProdutoCalculado { get; private set; }
    }
}
