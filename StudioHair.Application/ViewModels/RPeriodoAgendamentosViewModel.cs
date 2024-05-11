namespace StudioHair.Application.ViewModels
{
    public class RPeriodoAgendamentosViewModel
    {
        public RPeriodoAgendamentosViewModel(string nomeCliente, string telefoneCelular, string whatsapp, DateTime dia, string horaInicial, string horaFinal, decimal total)
        {
            NomeCliente = nomeCliente;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            Total = total;
        }

        public string NomeCliente { get; private set; }
        public string TelefoneCelular { get; private set; }
        public string Whatsapp { get; private set; }
        public DateTime Dia { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal Total { get; private set; }
    }
}
