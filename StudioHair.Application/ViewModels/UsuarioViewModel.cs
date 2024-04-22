namespace StudioHair.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(int codigo, string nome, string senha, string email, DateTime dataCadastro, bool ativo, string nivel)
        {
            Codigo = codigo;
            Nome = nome;
            Senha = senha;
            Email = email;
            DataCadastro = dataCadastro;
            Ativo = ativo;
            Nivel = nivel;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public string Nivel { get; set; }
    }
}
