using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class UpdateUsuarioInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(4, ErrorMessage = "Nome deve ter no minímo 4 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nível é obrigatório")]
        public string Nivel { get; set; }
    }
}
