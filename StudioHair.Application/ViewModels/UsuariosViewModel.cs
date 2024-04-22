namespace StudioHair.Application.ViewModels
{
    public class UsuariosViewModel
    {
        public UsuariosViewModel(int codigo, string nome, string email, string nivel, string status)
        {
            Codigo = codigo;
            Nome = nome;
            Email = email;
            Nivel = nivel;
            Status = status;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Nivel { get; private set; }
        public string Status { get; set; }
    }
}
