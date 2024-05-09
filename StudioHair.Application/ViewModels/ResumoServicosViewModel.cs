namespace StudioHair.Application.ViewModels
{
    public class ResumoServicosViewModel
    {
        public ResumoServicosViewModel(int servicosCriados)
        {
            ServicosCriados = servicosCriados;
        }
        public int ServicosCriados { get; private set; }
    }
}
