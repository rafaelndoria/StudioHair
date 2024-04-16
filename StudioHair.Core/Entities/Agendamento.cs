using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Agendamento : Entidade
    {
        public Agendamento(string nome, DateTime dia, string horaInicial, string horaFinal, decimal valorProfissional, int clienteId)
        {
            Nome = nome;
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            ValorProfissional = valorProfissional;
            ClienteId = clienteId;

            AgendamentoServicos = new List<AgendamentoServicos>();
        }

        public string Nome { get; private set; }
        public DateTime Dia { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal ValorProfissional { get; private set; }
        public EAgendamento Status { get; private set; }
        public decimal? ValorAgendamento { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int ClienteId { get; private set; }

        public Cliente? Cliente { get; set; }
        public List<AgendamentoServicos> AgendamentoServicos { get; set; }

        public void AdicionarValorAgendamento(decimal valor)
        {
            ValorAgendamento = (ValorAgendamento - ValorDesconto) + valor;
        }

        public void AdicionarValorDesconto(decimal valor)
        {
            if (ValorDesconto + valor <= ValorAgendamento)
                throw new Exception("O valor do desconto não pode ser maior que o valor do agendamento.");
            ValorDesconto += valor;
        }

        public void RemoverDesconto()
        {
            ValorDesconto = 0;
        }

        public void AlterarStatus(EAgendamento status)
        {
            Status = status;
        }

        public void AlterarValorProfissional(decimal valor)
        {
            ValorProfissional = valor;
        }

        public void Atualizar(string nome, DateTime dia, string horaInicial, string horaFinal)
        {
            Nome = nome;
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }
    }
}
