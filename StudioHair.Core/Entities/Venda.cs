using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Venda : Entidade
    {
        public Venda(ETipoPagamento tipoPagamento, int clienteId)
        {
            ClienteId = clienteId;
            TipoPagamento = tipoPagamento;

            DataDaVenda = DateTime.Now;
            ValorDesconto = 0;
            Total = 0;

            ProdutosVendas = new List<ProdutosVenda>();
        }

        public DateTime DataDaVenda { get; private set; }
        public ETipoPagamento TipoPagamento { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public decimal? Total { get; private set; }
        public int ClienteId { get; private set; }

        public List<ProdutosVenda> ProdutosVendas { get; set; }

        public void AdicionarValor(decimal valor)
        {
            Total += valor;
        }

        public void AplicarDesconto(decimal valor)
        {
            if (Total < 0)
            {
                throw new InvalidOperationException("Não é possível aplicar desconto sem valor de venda.");
            }
            else
            {
                if (Total - valor < 0)
                {
                    throw new InvalidOperationException("O valor do desconto é maior que o total da venda.");
                }
                else
                {
                    Total -= valor;
                }
            }
        }
    }
}
