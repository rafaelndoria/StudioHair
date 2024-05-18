using StudioHair.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class CadastroAgendamentoInputModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dia { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A hora inicial é obrigatória")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string HoraInicial { get; set; }

        [Required(ErrorMessage = "A hora final é obrigatória")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string HoraFinal { get; set; }

        [Required(ErrorMessage = "O valor do profissional é obrigatório")]
        public string ValorProfissional { get; set; }

        public string? ValorTotal { get; set; }

        public string? ValorDesconto { get; set; }

        public string? ValorServicos { get; set; }

        public int AdicionarServico { get; set; }

        public int AgendamentoId { get; set; }

        public int ServicoId { get; set; }

        public IEnumerable<ClienteVendaViewModel> Clientes { get; set; } = new List<ClienteVendaViewModel>();

        public IEnumerable<ServicoListViewModel> Servicos { get; set; } = new List<ServicoListViewModel>();

        public IEnumerable<ServicosAgendamentoViewModel> ServicosAgendamentos { get; set; } = new List<ServicosAgendamentoViewModel>();

        public IEnumerable<ValidationResult> Validate()
        {
            if (HoraFinal != null && HoraInicial != null)
            {
                if (DateTime.Parse(HoraFinal) <= DateTime.Parse(HoraInicial))
                {
                    yield return new ValidationResult("A hora final deve ser posterior à hora inicial.", new[] { "HoraFinal" });
                }
            }
        }
    }
}