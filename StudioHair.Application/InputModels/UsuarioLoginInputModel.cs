using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class UsuarioLoginInputModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(4, ErrorMessage = "Senha deve ter no minimo 4 caracteres")]
        public string Senha { get; set; }
    }
}
