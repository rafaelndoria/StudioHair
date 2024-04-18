namespace StudioHair.Core.Entities
{
    public class Servico : Entidade
    {
        public Servico(string nome, int duracaoEmMinutos, decimal valorServico)
        {
            Nome = nome;
            DuracaoEmMinutos = duracaoEmMinutos;
            ValorServico = valorServico;

            ValorDosProdutos = 0;
            ValorTotal = 0;

            ProdutosServicos = new List<ProdutosServico>();
            AgendamentoServicos = new List<AgendamentoServicos>();
        }

        public string Nome { get; private set; }
        public int DuracaoEmMinutos { get; private set; }
        public decimal ValorServico { get; private set; }
        public decimal? ValorDosProdutos { get; private set; }
        public decimal? ValorTotal { get; private set; }

        public List<ProdutosServico>? ProdutosServicos { get; set; }
        public List<AgendamentoServicos> AgendamentoServicos { get; set; }

        public void AdicionarValorProduto(decimal valorProduto)
        {
            ValorDosProdutos += + valorProduto;
            AtualizarValorTotal();
        }

        private void AtualizarValorTotal()
        {
            ValorTotal = (ValorDosProdutos + ValorServico);
        }
    }
}
