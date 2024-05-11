namespace StudioHair.Application.ViewModels
{
    public class RelatorioPeriodoVendasViewModel
    {
        public RelatorioPeriodoVendasViewModel(string filtros, string usuario)
        {
            Filtros = filtros;
            Usuario = usuario;
        }

        public IEnumerable<RVendasPorPeriodoViewModel> VendasPorPeriodo { get; set; } = new List<RVendasPorPeriodoViewModel>();
        public string Filtros { get; private set; }
        public string Usuario { get; private set; }
    }
}
