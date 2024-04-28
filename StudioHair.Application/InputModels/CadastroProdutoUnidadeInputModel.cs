using StudioHair.Application.ViewModels;
using StudioHair.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroProdutoUnidadeInputModel
    {
        public int ProdutoId { get; set; }
        public string TipoUnidade { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que 0")]
        public int Quantidade { get; set; }
        public int Id { get; set; }

        public List<ProdutoUnidadeViewModel>? ProdutoUnidadeViewModel { get; set; } = new List<ProdutoUnidadeViewModel>();
    }
}
