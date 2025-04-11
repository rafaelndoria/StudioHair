namespace StudioHair.Application.ViewModels
{
    public class DetalhesDadosProdutosMaisProcuradosViewModel
    {
        public DetalhesDadosProdutosMaisProcuradosViewModel(string produto, int quantidadeDeVendasProduto, decimal valorTotalProduto)
        {
            Produto = produto;
            QuantidadeDeVendasProduto = quantidadeDeVendasProduto;
            ValorTotalProduto = valorTotalProduto;
        }

        public string Produto { get; private set; }
        public int QuantidadeDeVendasProduto { get; private set; }
        public decimal ValorTotalProduto { get; private set; }
    }
}
