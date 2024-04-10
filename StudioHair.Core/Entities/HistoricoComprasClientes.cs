namespace StudioHair.Core.Entities
{
    public class HistoricoComprasClientes : Entidade
    {
        public HistoricoComprasClientes(DateTime dia, int compraId)
        {
            Dia = dia;
            CompraId = compraId;
        }

        public DateTime Dia { get; private set; }
        public int CompraId { get; private set; }
    }
}
