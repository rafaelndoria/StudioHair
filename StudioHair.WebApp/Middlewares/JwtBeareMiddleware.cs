namespace StudioHair.WebApp.Middlewares
{
    public class JwtBeareMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBeareMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Verifica se há um token JWT na sessão
            var token = context.Session.GetString("Token");

            if (!string.IsNullOrEmpty(token))
            {
                // Adiciona o token ao cabeçalho de autorização
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }

            // Chama o próximo middleware no pipeline
            await _next(context);
        }
    }
}
