﻿using FoodDelivery.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        //para mostrar un mensaje mas personalizado, cuando se usa el fluValidation
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsModelState = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage))
                    .ToList();

                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Message = "Ingrese todos los campos requeridos";
                errorResponse.Errors = new List<ErrorValidationModel>();

                errorsModelState.ForEach(error =>
                {
                    foreach (var message in error.Value)
                    {
                        errorResponse.Errors.Add(new()
                        {
                            FieldName = error.Key,
                            Message = message
                        });
                    }
                });

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}
