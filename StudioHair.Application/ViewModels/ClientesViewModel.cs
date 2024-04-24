namespace StudioHair.Application.ViewModels
{
    public class ClientesViewModel
    {
        public ClientesViewModel(int codigo, string nome, string email, string telefoneCelular, string whatsapp, bool ativo)
        {
            Codigo = codigo;
            Nome = nome;
            Email = email;
            TelefoneCelular = telefoneCelular;
            Whatsapp = whatsapp;
            Ativo = ativo;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string TelefoneCelular { get; private set; }
        public string Whatsapp { get; private set; }
        public bool Ativo { get; private set; }
    }
}
