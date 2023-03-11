using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API_ShopColibri.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute,IAsyncActionFilter
    {
        //atributo para la seguridad de la API
        private const string NombreDelApiKey = "ColibriShop";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(NombreDelApiKey, out var ApiSalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No se ha incluído una API Key"
                };

                return;

            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apikey = appSettings.GetValue<string>(NombreDelApiKey);

            if (!apikey.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "La API Key suministrada no es la correcta."
                };

                return;
            }

            await next();

        }
    }
}
