using Food.Application.Cores.Exceptions;
using FoodDelivery.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace FoodDelivery.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        //se encarga de capturar excepciones que se produzcan durante el procesamiento de una solicitud HTTP y
        //responder con una respuesta de error JSON adecuada en función del tipo de excepción.

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var errorResult = new ErrorModel();
                HttpStatusCode statusCode;


                switch (exception)
                {
                    case NotFoundCoreException e:
                        _logger.LogWarning("NotFoundCoreException:: {exception}", exception.Message);
                        statusCode = HttpStatusCode.NotFound;
                        errorResult.Message = e.Message;
                        break;
                    default:
                        _logger.LogError("Exception:: {exception}", exception.Message);
                        statusCode = HttpStatusCode.InternalServerError;
                        errorResult.Message = "Se ha producido un error inesperado: " + exception.Message;                    
                        break;
                }
                var response = context.Response;

                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = (int)statusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }

            }
        }

    }
}
