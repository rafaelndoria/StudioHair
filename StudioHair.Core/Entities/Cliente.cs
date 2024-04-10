using StudioHair.Core.Enums;

namespace StudioHair.Core.Entities
{
    public class Cliente : Entidade
    {
        public Cliente(string email, string telefoneCelular, string whatsapp, string facebook, int frequenciaSalaoPorMes, DateTime dataCadastro, string observacao, int pessoaId, EStatus status)
        {
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            FrequenciaSalaoPorMes = frequenciaSalaoPorMes;
            DataCadastro = dataCadastro;
            Observacao = observacao;
            PessoaId = pessoaId;
            Status = status;
        }

        public string Email { get; private set; }
        public string TelefoneCelular { get; private set;}
        public string Whatsapp { get; private set; }
        public string Facebook { get; private set; }
        public int FrequenciaSalaoPorMes { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Observacao { get; private set; }
        public int PessoaId { get; private set; }
        public EStatus Status { get; private set; }
    }
}
