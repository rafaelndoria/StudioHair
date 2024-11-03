namespace StudioHair.Application.ViewModels
{
    public class ImagemProdutoViewModel
    {
        public ImagemProdutoViewModel(int imagemId, string caminho, string nomeProduto, int produtoId)
        {
            ImagemId = imagemId;
            Caminho = caminho;
            NomeProduto = nomeProduto;
            ProdutoId = produtoId;
        }

        public int ImagemId { get; private set; }
        public string Caminho { get; private set; }
        public string NomeProduto { get; private set; }
        public int ProdutoId { get; private set; }
    }
}
