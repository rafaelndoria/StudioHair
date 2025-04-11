namespace StudioHair.Application.ViewModels
{
    public class FaturamentoMensalDetalhesMesViewModel
    {
        public FaturamentoMensalDetalhesMesViewModel(decimal valorTotalVendas,
                                                     decimal valorTotalServicos,
                                                     int quantidadeTotalVendas,
                                                     int quantidadeTotalServicos,
                                                     string mesAno)
        {
            ValorTotalVendas = valorTotalVendas;
            ValorTotalServicos = valorTotalServicos;
            QuantidadeTotalVendas = quantidadeTotalVendas;
            QuantidadeTotalServicos = quantidadeTotalServicos;
            MesAno = mesAno;

            ValorTotal = ValorTotalVendas + ValorTotalServicos;
            QuantidadeTotal = QuantidadeTotalVendas + QuantidadeTotalServicos;
        }

        public decimal ValorTotalVendas { get; private set; }
        public decimal ValorTotalServicos { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int QuantidadeTotalVendas { get; private set; }
        public int QuantidadeTotalServicos { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public string MesAno { get; private set; }
    }
}
