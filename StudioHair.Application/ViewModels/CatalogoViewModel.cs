namespace StudioHair.Application.ViewModels
{
    public class CatalogoViewModel
    {
        public CatalogoViewModel(int paginaAtual, int totalPaginas, int tamanhoPagina)
        {
            PaginaAtual = paginaAtual;
            TotalPaginas = totalPaginas;
            TamanhoPagina = tamanhoPagina;
        }

        public List<CatalogoProdutoVendaViewModel> Produtos { get; set; } = new List<CatalogoProdutoVendaViewModel>();
        public int PaginaAtual { get; private set; }
        public int TotalPaginas { get; private set; }
        public int TamanhoPagina { get; private set; }
    }
}
