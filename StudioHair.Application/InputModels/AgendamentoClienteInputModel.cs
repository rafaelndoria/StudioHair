using StudioHair.Application.ViewModels;

namespace StudioHair.Application.InputModels
{
    public class AgendamentoClienteInputModel
    {
        public IEnumerable<HorariosDisponiveisViewModel> Horarios { get; set; } = new List<HorariosDisponiveisViewModel>();
        public DateTime Dia { get; set; } = DateTime.Now.Date;
        public string? HoraInicial { get; set; }
        public DateTime DataEscolhida { get; set; }
    }
}
