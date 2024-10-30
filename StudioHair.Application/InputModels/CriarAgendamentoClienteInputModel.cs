using StudioHair.Application.ViewModels;

namespace StudioHair.Application.InputModels
{
    public class CriarAgendamentoClienteInputModel
    {
        public IEnumerable<ServicoListViewModel> Servicos { get; set; } = new List<ServicoListViewModel>();
        public int UsuarioId { get; set; }
        public string HoraInicial { get; set; }
        public DateTime Dia { get; set; }
        public decimal ValorAgendamento { get; set; }
        public int ServicoSelecionado { get; set; }
        public List<int> ServicosAdicionados { get; set; } = new List<int>();
    }
}
