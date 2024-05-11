using Microsoft.AspNetCore.Http.Extensions;

namespace StudioHair.WebApp.Middlewares
{
    public class RedirectToPreviousPageMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToPreviousPageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Armazena a URL da página atual antes de processar a requisição
            var previousPageUrl = context.Request.GetEncodedUrl();

            await _next(context);

            // Verifica se o código de status da resposta é 403 (Forbidden)
            if (context.Response.StatusCode == 403)
            {
                // Armazena a mensagem de aviso na sessão
                context.Session.SetString("AccessDeniedMessage", "Você não tem permissão para acessar esta página.");

                // Obtém a URL da página anterior armazenada
                var storedUrl = context.Session.GetString("PreviousPageUrl");

                // Verifica se a URL da página anterior é válida
                if (!string.IsNullOrEmpty(storedUrl))
                {
                    // Redireciona o usuário de volta para a página anterior
                    context.Response.Redirect(storedUrl);
                }
                else
                {
                    // Se não houver uma URL anterior armazenada, redireciona para a página inicial
                    context.Response.Redirect("/");
                }
            }
        }
    }
}
