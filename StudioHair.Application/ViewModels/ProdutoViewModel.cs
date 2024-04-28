namespace StudioHair.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel(int codigo, string nome, string marca, decimal valorVendas)
        {
            Codigo = codigo;
            Nome = nome;
            Marca = marca;
            ValorVendas = valorVendas;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Marca { get; private set; }
        public decimal ValorVendas { get; private set; }
    }
}
