namespace StudioHair.Application.ViewModels
{
    public class ResumoProdutosViewModel
    {
        public ResumoProdutosViewModel(int produtosCriados, int controlaEstoque, int paraVenda)
        {
            ProdutosCriados = produtosCriados;
            ControlaEstoque = controlaEstoque;
            ParaVenda = paraVenda;
        }

        public int ProdutosCriados { get; private set; }
        public int ControlaEstoque { get; private set; }
        public int ParaVenda { get; private set; }
    }
}
