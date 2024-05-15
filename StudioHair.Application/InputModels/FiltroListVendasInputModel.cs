using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;

namespace StudioHair.Application.InputModels
{
    public class FiltroListVendasInputModel
    {
        public int ClienteId { get; set; }
        public string Periodo { get; set; }
        public DateTime Inicial { get; set; } = DateTime.Now;
        public DateTime Final { get; set; } = DateTime.Now;

        public IEnumerable<ClienteVendaViewModel> Clientes { get; set; } = new List<ClienteVendaViewModel>();
    }
}
