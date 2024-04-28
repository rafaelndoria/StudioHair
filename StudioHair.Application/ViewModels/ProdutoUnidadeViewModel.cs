namespace StudioHair.Application.ViewModels
{
    public class ProdutoUnidadeViewModel
    {
        public ProdutoUnidadeViewModel(int produtoId, int id, int quantidade, string tipoUnidade)
        {
            ProdutoId = produtoId;
            Id = id;
            Quantidade = quantidade;
            TipoUnidade = tipoUnidade;
        }

        public int ProdutoId { get; private set; }
        public int Id { get; private set; }
        public int Quantidade { get; private set; }
        public string TipoUnidade { get; private set; }
    }
}
