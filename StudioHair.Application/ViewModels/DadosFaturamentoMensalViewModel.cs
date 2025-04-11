using StudioHair.Core.Entities;

namespace StudioHair.Application.ViewModels
{
    public class DadosFaturamentoMensalViewModel
    {
        public DadosFaturamentoMensalViewModel(decimal valorTotalVendas, 
                                               decimal valorTotalServicos, 
                                               int quantidadeTotalVendas, 
                                               int quantidadeTotalServicos, 
                                               IEnumerable<FaturamentoMensalDetalhesMesViewModel> detalhesMesViewModels)
        {
            ValorTotalVendas = valorTotalVendas;
            ValorTotalServicos = valorTotalServicos;
            QuantidadeTotalVendas = quantidadeTotalVendas;
            QuantidadeTotalServicos = quantidadeTotalServicos;
            FaturamentoMensalDetalhesMes = detalhesMesViewModels;

            ValorTotal = ValorTotalVendas + ValorTotalServicos;
            QuantidadeTotal = QuantidadeTotalVendas + QuantidadeTotalServicos;
        }

        public decimal ValorTotal { get; private set; }
        public decimal ValorTotalVendas { get; private set; }
        public decimal ValorTotalServicos { get; private set; }
        public int QuantidadeTotalVendas  { get; private set; }
        public int QuantidadeTotalServicos  { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public IEnumerable<FaturamentoMensalDetalhesMesViewModel> FaturamentoMensalDetalhesMes { get; private set; }
    }
}
