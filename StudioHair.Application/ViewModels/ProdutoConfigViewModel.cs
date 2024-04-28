namespace StudioHair.Application.ViewModels
{
    public class ProdutoConfigViewModel
    {
        public ProdutoConfigViewModel(int codigo, string nome, string marca, string codigoDeBarras, decimal valorVendas, bool produtoVendas, bool controlaEstoque)
        {
            Codigo = codigo;
            Nome = nome;
            Marca = marca;
            CodigoDeBarras = codigoDeBarras;
            ValorVendas = valorVendas;
            ProdutoVendas = produtoVendas;
            ControlaEstoque = controlaEstoque;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Marca { get; private set; }
        public string CodigoDeBarras { get; private set; }
        public decimal ValorVendas { get; private set; }
        public bool ProdutoVendas { get; private set; }
        public bool ControlaEstoque { get; private set; }
    }
}
