using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class ProdutoUnidade : Entidade
    {
        public ProdutoUnidade(EUnidade unidade, string quantidade, int produtoId)
        {
            Unidade = unidade;
            Quantidade = quantidade;
            ProdutoId = produtoId;
        }

        public EUnidade Unidade { get; private set; }
        public string Quantidade { get; private set; }
        public int ProdutoId { get; private set; }

        public Produto? Produto { get; set; }

        public void Atualizar(EUnidade unidade, string quantidade)
        {
            Unidade = unidade;
            Quantidade = quantidade;
        }
    }
}
