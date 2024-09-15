namespace StudioHair.Core.Entities
{
    public class Pessoa : Entidade
    {
        public Pessoa(string nome, DateTime dataDeNascimento, string rua, string cep, string cidade, string bairro, string numero, string cpf)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Rua = rua;
            Cep = cep;
            Cidade = cidade;
            Bairro = bairro;
            Numero = numero;
            Cpf = cpf;
        }

        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Numero { get; private set; }
        public string Cpf { get; private set; }
        public int? UsuarioId { get; private set; }

        public Cliente? Cliente { get; set; }
        public Usuario? Usuario { get; private set; }

        public void Atualizar(string rua, string cep, string cidade, string bairro, string numero)
        {
            Rua = rua;
            Cep = cep;
            Cidade = cidade;
            Bairro = bairro;
            Numero = numero;
        }

        public void VincularUsuario(int id)
        {
            UsuarioId = id;
        }

        public void DesvincularUsuario()
        {
            UsuarioId = null;
        }
    }
}
