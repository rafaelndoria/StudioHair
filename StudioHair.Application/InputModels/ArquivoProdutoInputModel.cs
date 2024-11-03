using Microsoft.AspNetCore.Http;

namespace StudioHair.Application.InputModels
{
    public class ArquivoProdutoInputModel
    {
        public IFormFile Arquivo { get; set; }
        public int ProdutoId { get; set; }
    }
}
