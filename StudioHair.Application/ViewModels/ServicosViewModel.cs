namespace StudioHair.Application.ViewModels
{
    public class ServicosViewModel
    {
        public ServicosViewModel(int id, string nome, int duracaoEmMinutos, decimal valorSevico)
        {
            Id = id;
            Nome = nome;
            DuracaoEmMinutos = duracaoEmMinutos;
            ValorSevico = valorSevico;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int DuracaoEmMinutos { get; private set; }
        public decimal ValorSevico { get; private set; }
    }
}
