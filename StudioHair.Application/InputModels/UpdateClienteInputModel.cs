using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class UpdateClienteInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rua é obrigatória")]
        [MaxLength(50, ErrorMessage = "Rua deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Rua deve ter no minimo 3 caracteres")]
        public string PessoaRua { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve ter exatamente 8 caracteres")]
        public string PessoaCep { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [MaxLength(50, ErrorMessage = "Cidade deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Cidade deve ter no minimo 3 caracteres")]
        public string PessoaCidade { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [MaxLength(50, ErrorMessage = "Bairro deve ter no maximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "Bairro deve ter no minimo 3 caracteres")]
        public string PessoaBairro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        [MaxLength(10, ErrorMessage = "Número deve ter no maximo 10 caracteres")]
        [MinLength(1, ErrorMessage = "Número deve ter no minimo 1 caracteres")]
        public string PessoaNumero { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string ClienteEmail { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefone deve ter exatamente 11 caracteres")]
        public string ClienteTelefoneCelular { get; set; }

        [Required(ErrorMessage = "Whatsapp é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Whatsapp deve ter exatamente 11 caracteres")]
        public string ClienteWhatsapp { get; set; }

        [Required(ErrorMessage = "Facebook é obrigatório")]
        [MaxLength(50, ErrorMessage = "Facebook deve ter no máximo 50 caracteres")]
        public string ClienteFacebook { get; set; }

        public string? ClienteObservacao { get; set; }
    }
}
