namespace StudioHair.Application.ViewModels
{
    public class ProdutoVendaViewModel
    {
        public ProdutoVendaViewModel(int id, string nome, decimal valor)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
    }
}
