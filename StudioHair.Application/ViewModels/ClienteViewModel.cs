namespace StudioHair.Application.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel(int codigo, string nome, DateTime dataDeNascimento, string rua, string cep, string cidade, string bairro, string numero, string cpf, string email, string telefoneCelular, string whatsapp, string facebook, int frequencia, string observacao, DateTime dataCadastro, bool ativo, string nomeUsuarioVinculado)
        {
            Codigo = codigo;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Rua = rua;
            Cep = cep;
            Cidade = cidade;
            Bairro = bairro;
            Numero = numero;
            Cpf = cpf;
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            Frequencia = frequencia;
            Observacao = observacao;
            DataCadastro = dataCadastro;
            Ativo = ativo;
            NomeUsuarioVinculado = nomeUsuarioVinculado;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Numero { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string TelefoneCelular { get; private set; }
        public string Whatsapp { get; private set; }
        public string Facebook { get; private set; }
        public int Frequencia { get; private set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public string NomeUsuarioVinculado { get; private set; }
    }
}
