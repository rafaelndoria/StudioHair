namespace StudioHair.Application.ViewModels
{
    public class DadosServicosMaisProcuradosViewModel
    {
        public DadosServicosMaisProcuradosViewModel(string servicoMaisProcurado, int quantidadeServico, decimal valorTotalServico, IEnumerable<DateTime> datasServico, IEnumerable<DetalhesDadosServicosMaisProcuradosViewModel> detalhesServicos)
        {
            ServicoMaisProcurado = servicoMaisProcurado;
            QuantidadeServico = quantidadeServico;
            ValorTotalServico = valorTotalServico;
            DatasServico = datasServico;
            DetalhesServicos = detalhesServicos;
        }

        public string ServicoMaisProcurado { get; private set; }
        public int QuantidadeServico { get; private set; }
        public decimal ValorTotalServico { get; private set; }
        public IEnumerable<DateTime> DatasServico { get; private set; }
        public IEnumerable<DetalhesDadosServicosMaisProcuradosViewModel> DetalhesServicos { get; private set; }
    }
}
