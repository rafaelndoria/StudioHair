using System.ComponentModel.DataAnnotations;

namespace StudioHair.Application.InputModels
{
    public class DataAgendamentoConsultaInputModel
    {
        [Required(ErrorMessage = "Para pesquisar agendamentos é necessario informar uma data.")]
        public DateTime DataAgendamento { get; set; }
    }
}
