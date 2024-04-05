using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Venda : Entidade
    {
        public Venda(DateTime dataDaVenda, int clientId, ETipoPagamento tipoPagamento, decimal total, decimal valorDesconto)
        {
            DataDaVenda = dataDaVenda;
            ClientId = clientId;
            TipoPagamento = tipoPagamento;
            Total = total;
            ValorDesconto = valorDesconto;
        }

        public DateTime DataDaVenda { get; private set; }
        public int ClientId { get; private set; }
        public ETipoPagamento TipoPagamento { get; private set; }
        public decimal Total { get; private set; }
        public decimal ValorDesconto { get; private set; }
    }
}
