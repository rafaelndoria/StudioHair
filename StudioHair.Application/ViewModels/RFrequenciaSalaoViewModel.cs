namespace StudioHair.Application.ViewModels
{
    public class RFrequenciaSalaoViewModel
    {
        public RFrequenciaSalaoViewModel(string nomeCliente, int frequenciaNoPeriodo, decimal agendamentoMedio, decimal total)
        {
            NomeCliente = nomeCliente;
            FrequenciaNoPeriodo = frequenciaNoPeriodo;
            AgendamentoMedio = agendamentoMedio;
            Total = total;
        }

        public string NomeCliente { get; private set; }
        public int FrequenciaNoPeriodo { get; private set; }
        public decimal AgendamentoMedio { get; private set; }
        public decimal Total { get; private set; }
    }
}
