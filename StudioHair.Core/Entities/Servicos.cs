namespace StudioHair.Core.Entities
{
    public class Servicos : Entidade
    {
        public Servicos(string nome, int duracaoEmMinutos, int valorDosProdutos, decimal valoeServico)
        {
            Nome = nome;
            DuracaoEmMinutos = duracaoEmMinutos;
            ValorDosProdutos = valorDosProdutos;
            ValoeServico = valoeServico;
        }

        public string Nome { get; private set; }
        public int DuracaoEmMinutos{ get; private set; }
        public int ValorDosProdutos{ get; private set; }
        public decimal ValoeServico{ get; private set; }
    }
}
