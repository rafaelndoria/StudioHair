namespace StudioHair.Application.ViewModels
{
    public class ProdutoDetalheCatalogoViewModel
    {
        public ProdutoDetalheCatalogoViewModel(string nomeProduto, string descricao, string marca, decimal valor, int produtoId, string caminhoImagem)
        {
            NomeProduto = nomeProduto;
            Descricao = descricao;
            Marca = marca;
            Valor = valor;
            ProdutoId = produtoId;
            CaminhoImagem = caminhoImagem;
        }

        public string NomeProduto { get; private set; }
        public string Descricao { get; private set; }
        public string Marca { get; private set; }
        public decimal Valor { get; private set; }
        public int ProdutoId { get; private set; }
        public string CaminhoImagem { get; private set; }
    }
}
