namespace StudioHair.Application.ViewModels
{
    public class ResumoAgendamentosViewModel
    {
        public ResumoAgendamentosViewModel(int quantidadeAgendamentoDiario, int quantidadeProximosAgend, string proximoAgendamento, decimal totalAgendamentoDiario, decimal totalAgendamentoMensal)
        {
            QuantidadeAgendamentoDiario = quantidadeAgendamentoDiario;
            QuantidadeProximosAgend = quantidadeProximosAgend;
            ProximoAgendamento = proximoAgendamento;
            TotalAgendamentoDiario = totalAgendamentoDiario;
            TotalAgendamentoMensal = totalAgendamentoMensal;
        }

        public int QuantidadeAgendamentoDiario { get; private set; }
        public int QuantidadeProximosAgend { get; private set; }
        public string ProximoAgendamento { get; private set; }
        public decimal TotalAgendamentoDiario { get; private set; }
        public decimal TotalAgendamentoMensal { get; private set; }
    }
}
