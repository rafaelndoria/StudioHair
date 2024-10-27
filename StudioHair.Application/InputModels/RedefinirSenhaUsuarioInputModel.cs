using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class RedefinirSenhaUsuarioInputModel
    {
        [Required(ErrorMessage = "A senha antiga é obrigatória.")]
        public string SenhaAntiga { get; set; }

        [Required(ErrorMessage = "A senha nova é obrigatória.")]
        [MinLength(6, ErrorMessage = "Senha deve ter no minímo 6 caracteres")]
        public string SenhaNova { get; set; }
    }
}
