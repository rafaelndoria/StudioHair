namespace StudioHair.Core.Entities
{
    public class Pessoa : Entidade
    {
        public Pessoa(string nome, DateTime dataDeNascimento, string rua, string cep, string cidade, string bairro, string número, string cpf)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Rua = rua;
            Cep = cep;
            Cidade = cidade;
            Bairro = bairro;
            Número = número;
            Cpf = cpf;
        }

        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Número { get; private set; }
        public string Cpf { get; private set; }

        public Cliente? Cliente { get; set; }
    }
}
