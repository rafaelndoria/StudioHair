namespace StudioHair.Core.Entities
{
    public class ConfiguracaoSistema : Entidade
    {
        public ConfiguracaoSistema(string corPrimaria, string corSecundaria, string corFonte, int tamanhoFonte, bool temaDark, int usuarioId)
        {
            CorPrimaria = corPrimaria;
            CorSecundaria = corSecundaria;
            CorFonte = corFonte;
            TamanhoFonte = tamanhoFonte;
            TemaDark = temaDark;
            UsuarioId = usuarioId;
        }

        public string CorPrimaria { get; private set; }
        public string CorSecundaria { get; private set; }
        public string CorFonte { get; private set; }
        public int TamanhoFonte { get; private set; }
        public bool TemaDark { get; private set; }

        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public void Atualizar(string corPrimaria, string corSecundaria, string corFonte, int tamanhoFonte, bool temaDark)
        {
            CorPrimaria = corPrimaria;
            CorSecundaria = corSecundaria;
            CorFonte = corFonte;
            TamanhoFonte = tamanhoFonte;
            TemaDark = temaDark;
        }
    }
}
