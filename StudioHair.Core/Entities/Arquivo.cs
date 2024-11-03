namespace StudioHair.Core.Entities
{
    public class Arquivo : Entidade
    {
        public Arquivo(string nomeArquivo, string caminho, int produtoId)
        {
            NomeArquivo = nomeArquivo;
            Caminho = caminho;
            ProdutoId = produtoId;

            DataUpload = DateTime.Now; 
        }

        public string NomeArquivo { get; private set; }
        public string Caminho { get; private set; }
        public DateTime DataUpload { get; private set; }

        public int ProdutoId { get; private set; }
        public Produto? Produto { get; private set; }
    }
}
