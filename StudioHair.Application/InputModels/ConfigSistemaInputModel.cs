using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class ConfigSistemaInputModel
    {
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Cor primária inválida. Use um valor hexadecimal válido (#RRGGBB).")]
        public string CorPrimaria { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Cor secundária inválida. Use um valor hexadecimal válido (#RRGGBB).")]
        public string CorSecundaria { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Cor da fonte inválida. Use um valor hexadecimal válido (#RRGGBB).")]
        public string CorFonte { get; set; }

        [Range(16, 36, ErrorMessage = "Tamanho da fonte inválido. Use um valor entre 10 e 36.")]
        public int TamanhoFonte { get; set; }
        public bool TemaDark { get; set; }

        [Required(ErrorMessage = "O campo 'UsuárioId' é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}
