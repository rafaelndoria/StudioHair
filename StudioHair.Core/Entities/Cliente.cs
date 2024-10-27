namespace StudioHair.Core.Entities
{
    public class Cliente : Entidade
    {
        public Cliente(string email, string telefoneCelular, string whatsapp, string facebook, int frequenciaSalaoPorMes, string observacao, int pessoaId)
        {
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            FrequenciaSalaoPorMes = frequenciaSalaoPorMes;
            Observacao = observacao;
            PessoaId = pessoaId;

            Ativo = true;
            DataCadastro = DateTime.Now;

            Vendas = new List<Venda>();
            Agendamentos = new List<Agendamento>();
            HistoricoClientes = new List<HistoricoCliente>();
            HistoricoComprasClientes = new List<HistoricoComprasClientes>();
        }

        public string Email { get; private set; }
        public string TelefoneCelular { get; private set;}
        public string Whatsapp { get; private set; } 
        public string Facebook { get; private set; }
        public int FrequenciaSalaoPorMes { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string? Observacao { get; private set; }
        public bool Ativo { get; private set; }
        public int PessoaId { get; private set; }

        public Pessoa? Pessoa { get; set; }
        public List<Venda>? Vendas { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public List<HistoricoCliente> HistoricoClientes { get; set; }
        public List<HistoricoComprasClientes> HistoricoComprasClientes { get; set; }

        public void Atualizar(string email, string telefoneCelular, string whatsapp, string facebook, int frequenciaSalaoPorMes, string observacao)
        {
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Facebook = facebook;
            FrequenciaSalaoPorMes = frequenciaSalaoPorMes;
            Observacao = observacao;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
