using StudioHair.Application.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroVendaInputModel
    {
        public int VendaId { get; set; }
        public string? ValorTotal { get; set; }
        public string? ValorDesconto { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Cliente é obrigatório")]
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public string? ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public string? ValorTotalProduto { get; set; }
        public string TipoPagamento { get; set; }
        public int AdicionarProdutoVenda { get; set; }
        public List<ProdutosVendaViewModel> ProdutosVenda { get; set; } = new List<ProdutosVendaViewModel>();
        public IEnumerable<ClienteVendaViewModel> Clientes { get; set; } = new List<ClienteVendaViewModel>();
        public IEnumerable<ProdutoVendaViewModel> Produtos { get; set; } = new List<ProdutoVendaViewModel>();
    }
}
