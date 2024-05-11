namespace StudioHair.Application.ViewModels
{
    public class FiltrosVendaViewModel
    {
        public FiltrosVendaViewModel(string filtros)
        {
            Filtros = filtros;
        }

        public string Filtros { get; private set; }
    }
}
