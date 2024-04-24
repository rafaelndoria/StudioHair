using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroPessoaInputModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "Nome deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Nome deve ter no minimo 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve ter exatamente 8 caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [MaxLength(50, ErrorMessage = "Cidade deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Cidade deve ter no minimo 3 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Rua é obrigatória")]
        [MaxLength(50, ErrorMessage = "Rua deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Rua deve ter no minimo 3 caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [MaxLength(50, ErrorMessage = "Bairro deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Bairro deve ter no minimo 3 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        [MaxLength(10, ErrorMessage = "Número deve ter no maximo 10 caracteres")]
        [MinLength(1, ErrorMessage = "Número deve ter no minimo 1 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter exatamente 11 caracteres")]
        public string Cpf { get; set; }
    }
}
