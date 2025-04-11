namespace StudioHair.Application.ViewModels
{
    public class DetalhesHorarioPicoViewModel
    {
        public DetalhesHorarioPicoViewModel(string data, IEnumerable<DetalhesHoraHorarioPicoViewModel> detalhesHora)
        {
            Data = data;
            DetalhesHora = detalhesHora;
        }

        public string Data { get; private set; }
        public IEnumerable<DetalhesHoraHorarioPicoViewModel> DetalhesHora { get; private set; }
    }
}
