namespace StudioHair.Application.ViewModels
{
    public class ServicoListViewModel 
    {
        public ServicoListViewModel(int id, string nome, int duracao, decimal valor)
        {
            Id = id;
            Nome = nome;
            Duracao = duracao;
            Valor = valor;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Duracao { get; private set; }
        public decimal Valor { get; private set; }
    }
}
