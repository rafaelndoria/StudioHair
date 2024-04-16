using Microsoft.VisualBasic;
using StudioHair.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioHair.Core.Entities
{
    public class Agendamento : Entidade
    {
        public Agendamento(string nome, DateTime dia, string horaInicial, string horaFinal, decimal valorProfissional, decimal valorAgendamento, decimal valorDesconto, int clienteId)
        {
            Nome = nome;
            Dia = dia;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
            ValorProfissional = valorProfissional;
            ValorAgendamento = valorAgendamento;
            ValorDesconto = valorDesconto;
            ClienteId = clienteId;
        }

        public string Nome { get; private set; }
        public DateTime Dia { get; private set; }
        public string HoraInicial { get; private set; }
        public string HoraFinal { get; private set; }
        public decimal ValorProfissional { get; private set; }
        public decimal ValorAgendamento { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public int ClienteId { get; private set; }
        public EAgendamento Status { get; private set; }

    }
}
