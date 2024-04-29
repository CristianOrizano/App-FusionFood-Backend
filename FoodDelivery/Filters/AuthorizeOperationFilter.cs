using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoodDelivery.Filters
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        //Si una operación tiene el atributo AllowAnonymous, no se requiere autenticación,
        //pero en caso contrario, se indica que se requiere un token JWT (Bearer Token) para acceder a la operación.
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var noAuthRequired = context.ApiDescription
                .CustomAttributes()
                .Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (noAuthRequired) return;

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                }
            };

        }
    }
}
