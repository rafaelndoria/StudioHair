using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class ProdutoUnidade : Entidade
    {
        public ProdutoUnidade(EUnidade unidade, string quantidade, string produtoID)
        {
            Unidade = unidade;
            Quantidade = quantidade;
            ProdutoID = produtoID;
        }

        public EUnidade Unidade { get; private set; }
        public string Quantidade { get; private set; }
        public string ProdutoID { get; private set; }
    }
}
