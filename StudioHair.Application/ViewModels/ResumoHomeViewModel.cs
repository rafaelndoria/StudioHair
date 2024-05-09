namespace StudioHair.Application.ViewModels
{
    public class ResumoHomeViewModel
    {
        public ResumoHomeViewModel(ResumoVendasViewModel resumoVendas, ResumoAgendamentosViewModel resumoAgendamentos, ResumoProdutosViewModel resumoProdutos, ResumoServicosViewModel resumoServicos)
        {
            ResumoVendas = resumoVendas;
            ResumoAgendamentos = resumoAgendamentos;
            ResumoProdutos = resumoProdutos;
            ResumoServicos = resumoServicos;
        }

        public ResumoVendasViewModel ResumoVendas { get; private set; }
        public ResumoAgendamentosViewModel ResumoAgendamentos { get; private set; }
        public ResumoProdutosViewModel ResumoProdutos { get; private set; }
        public ResumoServicosViewModel ResumoServicos { get; private set; }
    }
}
