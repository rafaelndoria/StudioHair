namespace StudioHair.Application.InputModels
{
    public class AtualizarServicoInputModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DuracaoMinutos { get; set; }
        public decimal ValorServico { get; set; }
    }
}
