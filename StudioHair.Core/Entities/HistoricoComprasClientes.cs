namespace StudioHair.Core.Entities
{
    public class HistoricoComprasClientes : Entidade
    {
        public HistoricoComprasClientes(DateTime dia, decimal valor, int clienteId)
        {
            Dia = dia;
            ClienteId = clienteId;
            Valor = valor;
        }

        public DateTime Dia { get; private set; }
        public int ClienteId { get; private set; }
        public decimal Valor { get; set; }
    }
}
