using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Usuario : Entidade
    {
        // construtor para cria adicionar um usuario padrao admin no sistema ao criar a migration
        public Usuario(int id, string nome, string email, EPapelUsuario papel, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Papel = papel;
            Senha = senha;

            Ativo = true;
            DataDeCadastro = DateTime.Now;
        }

        public Usuario(string nome, string email, EPapelUsuario papel, string senha)
        {
            Nome = nome;
            Email = email;
            Papel = papel;
            Senha = senha;

            Ativo = true;
            DataDeCadastro = DateTime.Now;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public EPapelUsuario Papel { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataDeCadastro { get; private set; }

        public Pessoa? Pessoa { get; private set; }
        public ConfiguracaoSistema ConfiguracaoSistema { get; private set; }

        public void AtualizarUsuario(string nome, string email, EPapelUsuario papel)
        {
            Nome = nome;
            Email = email;
            Papel = papel;
        }

        public void InativarUsuario()
        {
            Ativo = false;
        }

        public void AtivarUsuario()
        {
            Ativo = true;
        }

        public void RedefinirSenha(string senha)
        {
            Senha = senha;
        }
    }
}
