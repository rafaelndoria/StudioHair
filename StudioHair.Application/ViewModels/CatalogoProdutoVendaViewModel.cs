namespace StudioHair.Application.ViewModels
{
    public class CatalogoProdutoVendaViewModel
    {
        public CatalogoProdutoVendaViewModel(int produtoId, string caminhoArquivo, string nomeProduto, decimal valorProduto)
        {
            ProdutoId = produtoId;
            CaminhoArquivo = caminhoArquivo;
            NomeProduto = nomeProduto;
            ValorProduto = valorProduto;
        }

        public int ProdutoId { get; private set; }
        public string CaminhoArquivo { get; private set; }
        public string NomeProduto { get; private set; }
        public decimal ValorProduto { get; private set; }
    }
}
