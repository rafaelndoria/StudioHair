using StudioHair.Application.ViewModels;
using StudioHair.Core.Enums;

namespace StudioHair.Application.InputModels
{
    public class FiltroAnaliseInputModel
    {
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
        public IEnumerable<ClientesViewModel> Clientes { get; set; }
        public IEnumerable<ServicosViewModel> Servicos { get; set; }
        public ETipoAnaliseGerencial TipoAnalise { get; set; }
        public string DescricaoRelatorio { get; set; }
        public int ProdutoId { get; set; }
        public int ServicoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string TipoPeriodo { get; set; }
    }
}
