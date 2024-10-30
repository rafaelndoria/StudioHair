namespace StudioHair.Application.ViewModels
{
    public class ResumoAgendamentoViewModel
    {
        public ResumoAgendamentoViewModel(DateTime dia, string horaInicial, string horaFinal, decimal valorAgendamento, int agendamentoId)
        {
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            ValorAgendamento = valorAgendamento;
            AgendamentoId = agendamentoId;
        }

        public DateTime Dia { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal ValorAgendamento { get; private set; }
        public int AgendamentoId { get; private set; }
        public List<ServicosAgendamentoViewModel> Servicos { get; private set; } = new List<ServicosAgendamentoViewModel>();
    }
}
