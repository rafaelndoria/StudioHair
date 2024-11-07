namespace StudioHair.Core.Entities
{
    public class CarrinhoItem : Entidade
    {
        public CarrinhoItem(int quantidade, decimal valor, int produtoId, int carrinhoId)
        {
            Quantidade = quantidade;
            Valor = valor;
            ProdutoId = produtoId;
            CarrinhoId = carrinhoId;
        }

        public int Quantidade { get; private set; }
        public decimal Valor { get; private set; }
        public int ProdutoId { get; private set; }
        public int CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public Produto? Produto { get; set; }
    }
}
