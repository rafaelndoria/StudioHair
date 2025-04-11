namespace StudioHair.Application.ViewModels
{
    public class DadosHorarioPicoViewModel
    {
        public DadosHorarioPicoViewModel(IEnumerable<DetalhesHorarioPicoViewModel> detalhesHorarioPico)
        {
            DetalhesHorarioPico = detalhesHorarioPico;
        }

        public IEnumerable<DetalhesHorarioPicoViewModel> DetalhesHorarioPico { get; private set; }
    }
}
