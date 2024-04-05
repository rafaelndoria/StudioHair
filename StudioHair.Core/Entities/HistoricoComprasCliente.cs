namespace StudioHair.Core.Entities
{
    public class HistoricoComprasCliente : Entidade
    {
        public HistoricoComprasCliente(DateTime dia, int compraId)
        {
            Dia = dia;
            CompraId = compraId;
        }

        public DateTime Dia { get; set; }
        public int CompraId { get; set; }
    }
}
