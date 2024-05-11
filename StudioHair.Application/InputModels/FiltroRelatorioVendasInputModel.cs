using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;

namespace StudioHair.Application.InputModels
{
    public class FiltroRelatorioVendasInputModel
    {
        public string Periodo { get; set; }
        public DateTime Inicial { get; set; } = DateTime.Now;
        public DateTime Final { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public IEnumerable<ClientesViewModel> Clientes { get; set; }
    }
}
