using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class RedefinirSenhaInputModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Senha deve ter no minímo 6 caracteres")]
        public string Senha { get; set; }
    }
}
