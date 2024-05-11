namespace StudioHair.Application.ViewModels
{
    public class FiltrosViewModel
    {
        public FiltrosViewModel(string filtros)
        {
            Filtros = filtros;
        }

        public string Filtros { get; private set; }
    }
}
