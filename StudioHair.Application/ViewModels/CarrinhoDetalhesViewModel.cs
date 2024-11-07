namespace StudioHair.Application.ViewModels
{
    public class CarrinhoDetalhesViewModel
    {
        public CarrinhoDetalhesViewModel(decimal valorTotal)
        {
            ValorTotal = valorTotal;
        }

        public decimal ValorTotal { get; private set; }
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();
    }
}
