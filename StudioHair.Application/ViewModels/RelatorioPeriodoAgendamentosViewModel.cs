namespace StudioHair.Application.ViewModels
{
    public class RelatorioPeriodoAgendamentosViewModel
    {
        public RelatorioPeriodoAgendamentosViewModel(string filtros, string usuario)
        {
            Filtros = filtros;
            Usuario = usuario;
        }

        public IEnumerable<RPeriodoAgendamentosViewModel> AgendamentosPorPeriodo { get; set; } = new List<RPeriodoAgendamentosViewModel>();
        public string Filtros { get; private set; }
        public string Usuario { get; private set; }
    }
}
