using StudioHair.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroUsuarioInputModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(4, ErrorMessage = "Nome deve ter no minímo 4 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage ="Senha deve ter no minímo 6 caracteres")]
        [MaxLength(20, ErrorMessage ="Senha deve ter no maximo 20 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Nível é obrigatório")]
        public EPapelUsuario Papel { get; set; }
    }
}
