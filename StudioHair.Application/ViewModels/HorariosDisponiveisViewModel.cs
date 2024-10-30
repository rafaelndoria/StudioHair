namespace StudioHair.Application.ViewModels
{
    public class HorariosDisponiveisViewModel
    {
        public HorariosDisponiveisViewModel(string horarioInicial, string horarioFinal)
        {
            HorarioInicial = horarioInicial;
            HorarioFinal = horarioFinal;
        }

        public string HorarioInicial { get; private set; }
        public string HorarioFinal { get; private set; }
    }
}
