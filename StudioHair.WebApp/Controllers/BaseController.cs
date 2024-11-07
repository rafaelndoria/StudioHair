using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Recuperar as configurações da sessão e definir no ViewBag
        ViewBag.CorPrimaria = context.HttpContext.Items["CorPrimaria"] ?? "#ffc0cb";
        ViewBag.CorSecundaria = context.HttpContext.Items["CorSecundaria"] ?? "#f8f9fa";
        ViewBag.CorFonte = context.HttpContext.Items["CorFonte"] ?? "#212529";
        ViewBag.TamanhoFonte = context.HttpContext.Items["TamanhoFonte"] ?? "16";
        ViewBag.TemaDark = context.HttpContext.Items["TemaDark"] ?? "false";

        // Carrinho
        ViewBag.QuantidadeItensCarrinho = context.HttpContext.Session.GetString("QuantidadeItensCarrinho") ?? "0";

        base.OnActionExecuting(context);
    }
}
    