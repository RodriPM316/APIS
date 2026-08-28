namespace Api.Empleados
{
    public class BloqueaPeticionMiddleware
    {
        private readonly RequestDelegate next;

        public BloqueaPeticionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/bloqueado")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Acceso denegado");
                return;
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }

    public static class BloqueaPeticionMiddlewareExtensions
    {
        public static IApplicationBuilder UseBloqueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BloqueaPeticionMiddleware>();
        }
    }
}
