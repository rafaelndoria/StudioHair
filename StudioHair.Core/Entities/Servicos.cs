namespace StudioHair.Core.Entities
{
    public class Servicos : Entidade
    {
        public Servicos(string nome, int duracaoEmMinutos, decimal valorServico)
        {
            Nome = nome;
            DuracaoEmMinutos = duracaoEmMinutos;
            ValorServico = valorServico;

            ValorDosProdutos = 0;
            ValorTotal = 0;
        }

        public string Nome { get; private set; }
        public int DuracaoEmMinutos { get; private set; }
        public decimal ValorServico { get; private set; }
        public decimal? ValorDosProdutos { get; private set; }
        public decimal? ValorTotal { get; private set; }

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
