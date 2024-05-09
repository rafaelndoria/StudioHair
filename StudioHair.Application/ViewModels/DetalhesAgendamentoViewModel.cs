namespace StudioHair.Application.ViewModels
{
    public class DetalhesAgendamentoViewModel
    {
        public DetalhesAgendamentoViewModel(int codigo, string nomeAgendamento, DateTime dia, string horaInicial, string horaFinal, decimal valorProfissional, decimal valorAgendamento, string nomeCliente, string numero, string whatsapp)
        {
            Codigo = codigo;
            NomeAgendamento = nomeAgendamento;
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            ValorProfissional = valorProfissional;
            ValorAgendamento = valorAgendamento;
            NomeCliente = nomeCliente;
            Numero = numero;
            Whatsapp = whatsapp;
        }

        public int Codigo { get; private set; }
        public string NomeAgendamento { get; private set; }
        public DateTime Dia { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal ValorProfissional { get; private set; }
        public decimal ValorAgendamento { get; private set; }
        public string NomeCliente { get; private set; }
        public string Numero { get; private set; }
        public string Whatsapp { get; private set; }

        public IEnumerable<ServicosAgendamentoViewModel> ServicosAgendamento { get; set; } = new List<ServicosAgendamentoViewModel>();
    }
}
