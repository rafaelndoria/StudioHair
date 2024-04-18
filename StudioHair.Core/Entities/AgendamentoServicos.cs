namespace StudioHair.Core.Entities
{
    public class AgendamentoServicos : Entidade
    {
        public AgendamentoServicos(int agendamentoId, int servicoId)
        {
            AgendamentoId = agendamentoId;
            ServicoId = servicoId;
        }

        public int AgendamentoId { get; private set; }
        public int ServicoId { get; private set; }
        
        public Servico? Servico { get; set; }
    }
}
