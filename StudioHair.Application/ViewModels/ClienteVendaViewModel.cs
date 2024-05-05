namespace StudioHair.Application.ViewModels
{
    public class ClienteVendaViewModel
    {
        public ClienteVendaViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
    }
}
