namespace StudioHair.Application.ViewModels
{
    public class AgendamentosViewModel
    {
        public AgendamentosViewModel(int id, DateTime dia, string nomeCliente, string horaInicial, string horaFinal, decimal valor)
        {
            Id = id;
            Dia = dia;
            NomeCliente = nomeCliente;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            Valor = valor;
        }

        public int Id { get; private set; }
        public DateTime Dia { get; private set; }
        public string NomeCliente { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal Valor { get; private set; }
    }
}
