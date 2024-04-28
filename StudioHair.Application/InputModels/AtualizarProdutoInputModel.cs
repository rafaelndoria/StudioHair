using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class AtualizarProdutoInputModel
    {
        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho maximo do nome do produto é 100")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Marca do produto é obrigatório")]
        [MaxLength(40, ErrorMessage = "Tamanho maximo do nome do produto é 40")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Código de barras do produto é obrigatório")]
        [StringLength(13, ErrorMessage = "O código de barras deve conter exatamente 13 caracteres", MinimumLength = 13)]
        public string CodigoBarras { get; set; }

        [Required(ErrorMessage = "Valor de venda é obrigatório")]
        public string ValorVendas { get; set; }

        public bool ProdutoParaVenda { get; set; }
        public bool ControlaEstoque { get; set; }
        public int ProdutoId { get; set; }
    }
}
