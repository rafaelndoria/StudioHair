using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class HistoricoCliente : Entidade
    {
        public HistoricoCliente(DateTime dia, string nomeAgendamento, decimal valorAgendamento, EStatusAgendamento statusAgendamento, int clienteId)
        {
            Dia = dia;
            NomeAgendamento = nomeAgendamento;
            ValorAgendamento = valorAgendamento;
            StatusAgendamento = statusAgendamento;
            ClienteId = clienteId;
        }

        public DateTime Dia { get; private set; }
        public string NomeAgendamento { get; private set; }
        public decimal ValorAgendamento { get; private set; }
        public EStatusAgendamento StatusAgendamento { get; private set; }
        public int ClienteId { get; private set; }

        public Cliente? Cliente { get; set; }

        public void AlterarStatus(EStatusAgendamento status)
        {
            StatusAgendamento = status;
        }
    }
}
