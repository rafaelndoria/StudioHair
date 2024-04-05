namespace StudioHair.Core.Entities
{
    public class Usuario : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Papel { get; private set; }
        public string Senha { get; private set; }
    }
}
