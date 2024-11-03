namespace StudioHair.Core.Entities
{
    public class Produto : Entidade 
    {
        public Produto(string nome, string marca, string codigoBarras, decimal valorPraticado, bool produtoParaVenda, bool controlaEstoque)
        {
            Nome = nome;
            Marca = marca;
            CodigoBarras = codigoBarras;
            ValorPraticado = valorPraticado;
            ProdutoParaVenda = produtoParaVenda;
            ControlaEstoque = controlaEstoque;

            ProdutoUnidades = new List<ProdutoUnidade>();
            ProdutosVendas = new List<ProdutosVenda>();
            ProdutosServicos = new List<ProdutosServico>();
        }

        public Produto(int id, string nome, string marca, string codigoBarras, decimal valorPraticado, bool produtoParaVenda, bool controlaEstoque)
        {
            Id = id;
            Nome = nome;
            Marca = marca;
            CodigoBarras = codigoBarras;
            ValorPraticado = valorPraticado;
            ProdutoParaVenda = produtoParaVenda;
            ControlaEstoque = controlaEstoque;

            ProdutoUnidades = new List<ProdutoUnidade>();
            ProdutosVendas = new List<ProdutosVenda>();
            ProdutosServicos = new List<ProdutosServico>();
            Arquivos = new List<Arquivo>();
        }

        public string Nome { get; private set; }
        public string Marca { get; private set; }
        public string CodigoBarras { get; private set; }
        public decimal ValorPraticado { get; private set; }
        public bool ProdutoParaVenda { get; private set; }
        public bool ControlaEstoque { get; private set; }

        public List<ProdutoUnidade> ProdutoUnidades { get; set; }
        public List<ProdutosVenda> ProdutosVendas { get; set; }
        public List<ProdutosServico> ProdutosServicos { get; set; }
        public List<Arquivo> Arquivos { get; set; }

        public void Atualizar(string nome, string marca, string codigoBarras, decimal valorPraticado, bool produtoParaVenda, bool controlaEstoque)
        {
            Nome = nome;
            Marca = marca;
            CodigoBarras = codigoBarras;
            ValorPraticado = valorPraticado;
            ProdutoParaVenda = produtoParaVenda;
            ControlaEstoque = controlaEstoque;
        }
    }
}
