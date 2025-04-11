namespace StudioHair.Application.ViewModels
{
    public class DadosProdutosMaisVendidosViewModel
    {
        public DadosProdutosMaisVendidosViewModel(string produtoMaisVedido, int quantidadeDeVendasProduto, decimal valorTotalProduto, IEnumerable<DateTime> datas, IEnumerable<DetalhesDadosProdutosMaisProcuradosViewModel> detalhesProdutos)
        {
            ProdutoMaisVedido = produtoMaisVedido;
            QuantidadeDeVendasProduto = quantidadeDeVendasProduto;
            ValorTotalProduto = valorTotalProduto;
            Datas = datas;
            DetalhesProdutos = detalhesProdutos;
        }

        public string ProdutoMaisVedido { get; private set; }
        public int QuantidadeDeVendasProduto { get; private set; }
        public decimal ValorTotalProduto { get; private set; }
        public IEnumerable<DateTime> Datas { get; private set; }
        public IEnumerable<DetalhesDadosProdutosMaisProcuradosViewModel> DetalhesProdutos { get; private set; }
    }
}
