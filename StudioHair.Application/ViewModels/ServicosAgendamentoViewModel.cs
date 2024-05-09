namespace StudioHair.Application.ViewModels
{
    public class ServicosAgendamentoViewModel
    {
        public ServicosAgendamentoViewModel(string nome, int duracaoMinutos, decimal valor)
        {
            Nome = nome;
            DuracaoMinutos = duracaoMinutos;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public int DuracaoMinutos { get; private set; }
        public decimal Valor { get; private set; }
    }
}
