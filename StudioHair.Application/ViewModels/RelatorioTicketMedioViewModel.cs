namespace StudioHair.Application.ViewModels
{
    public class RelatorioTicketMedioViewModel
    {
        public RelatorioTicketMedioViewModel(string filtros, string usuario)
        {
            Filtros = filtros;
            Usuario = usuario;
        }

        public IEnumerable<RTicketMedioViewModel> TicketMedio { get; set; } = new List<RTicketMedioViewModel>();
        public string Filtros { get; private set; }
        public string Usuario { get; private set; }
    }
}
