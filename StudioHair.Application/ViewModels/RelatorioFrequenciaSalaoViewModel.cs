namespace StudioHair.Application.ViewModels
{
    public class RelatorioFrequenciaSalaoViewModel
    {
        public RelatorioFrequenciaSalaoViewModel(string filtros, string usuario)
        {
            Filtros = filtros;
            Usuario = usuario;
        }

        public IEnumerable<RFrequenciaSalaoViewModel> FrequenciasSalao { get; set; } = new List<RFrequenciaSalaoViewModel>();
        public string Filtros { get; private set; }
        public string Usuario { get; private set; }
    }
}
