using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Cliente : Entidade
    {
        public Cliente(string email, string telefoneCelular, string whatsapp, string facebook, int frequenciaSalaoPorMes, string observacao, int pessoaId, EStatus status)
        {
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            FrequenciaSalaoPorMes = frequenciaSalaoPorMes;
            Observacao = observacao;
            PessoaId = pessoaId;
            Status = status;

            Ativo = true;
            DataCadastro = DateTime.Now;
        }

        public string Email { get; private set; }
        public string TelefoneCelular { get; private set;}
        public string Whatsapp { get; private set; }
        public string Facebook { get; private set; }
        public int FrequenciaSalaoPorMes { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Observacao { get; private set; }
        public EStatus Status { get; private set; }
        public bool Ativo { get; private set; }
        public int PessoaId { get; private set; }

        public void Update(string email, string telefoneCelular, string whatsapp, string facebook, int frequenciaSalaoPorMes, string observacao)
        {
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            FrequenciaSalaoPorMes = frequenciaSalaoPorMes;
            Observacao = observacao;
        }

        public void AtivarPessoa()
        {
            Ativo = true;
        }

        public void InativarPessoa()
        {
            Ativo = false;
        }
    }
}
