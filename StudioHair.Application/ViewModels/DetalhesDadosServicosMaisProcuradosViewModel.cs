namespace StudioHair.Application.ViewModels
{
    public class DetalhesDadosServicosMaisProcuradosViewModel
    {
        public DetalhesDadosServicosMaisProcuradosViewModel(string servico, int quantidadeServico, decimal valorTotalServico)
        {
            Servico = servico;
            QuantidadeServico = quantidadeServico;
            ValorTotalServico = valorTotalServico;
        }

        public string Servico { get; private set; }
        public int QuantidadeServico { get; private set; }
        public decimal ValorTotalServico { get; private set; }
    }
}
