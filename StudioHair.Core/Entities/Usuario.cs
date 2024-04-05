namespace StudioHair.Core.Entities
{
    public class Usuario : Entidade
    {
        public Usuario(string nome, string email, string papel, string senha)
        {
            Nome = nome;
            Email = email;
            Papel = papel;
            Senha = senha;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Papel { get; private set; }
        public string Senha { get; private set; }
    }
}
