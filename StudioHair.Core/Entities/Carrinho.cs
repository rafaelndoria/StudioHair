namespace StudioHair.Core.Entities
{
    public class Carrinho : Entidade
    {
        public Carrinho(int clienteId)
        {
            ClienteId = clienteId;
        }

        public int ClienteId { get; private set; }
        public Cliente? Cliente { get; set; }
        public List<CarrinhoItem> CarrinhoItems { get; set; } = new List<CarrinhoItem>();
    }
}
