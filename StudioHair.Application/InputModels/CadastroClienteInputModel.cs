﻿using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroClienteInputModel
    {
        public int PessoaId { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefone deve ter exatamente 11 caracteres")]
        public string TelefoneCelular { get; set; }

        [Required(ErrorMessage = "Whatsapp é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Whatsapp deve ter exatamente 11 caracteres")]
        public string Whatsapp { get; set; }

        [Required(ErrorMessage = "Facebook é obrigatório")]
        [MaxLength(50, ErrorMessage = "Facebook deve ter no máximo 50 caracteres")]
        public string Facebook { get; set; }

        [MaxLength(150, ErrorMessage = "Observação deve ter no máximo 150 caracteres")]
        public string? Observacao { get; set; }
    }
}
