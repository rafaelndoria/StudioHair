namespace StudioHair.Application.ViewModels
{
    public class DetalhesHoraHorarioPicoViewModel
    {
        public DetalhesHoraHorarioPicoViewModel(string hora, int quantidadeTotal)
        {
            Hora = hora;
            QuantidadeTotal = quantidadeTotal;
        }

        public string Hora { get; private set; }
        public int QuantidadeTotal { get; private set; }
    }
}
