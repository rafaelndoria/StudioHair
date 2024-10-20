namespace StudioHair.WebApp.Middlewares
{
    public class ConfiguracaoSistemaMiddleware
    {
        private readonly RequestDelegate _next;

        public ConfiguracaoSistemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Recuperar as configurações da sessão
            var corPrimaria = context.Session.GetString("CorPrimaria") ?? "#ffc0cb";
            var corSecundaria = context.Session.GetString("CorSecundaria") ?? "#f8f9fa";
            var corFonte = context.Session.GetString("CorFonte") ?? "#212529";
            var tamanhoFonte = context.Session.GetString("TamanhoFonte") ?? "16";
            var temaDark = context.Session.GetString("TemaDark") ?? "false";

            // Definir as configurações no ViewBag para uso em todas as views
            context.Items["CorPrimaria"] = corPrimaria;
            context.Items["CorSecundaria"] = corSecundaria;
            context.Items["CorFonte"] = corFonte;
            context.Items["TamanhoFonte"] = tamanhoFonte;
            context.Items["TemaDark"] = temaDark;

            await _next(context);
        }
    }
}
