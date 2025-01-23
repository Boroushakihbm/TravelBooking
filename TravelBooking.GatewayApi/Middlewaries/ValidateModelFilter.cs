using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Text.Json;

namespace TravelBooking.GatewayApi.Middlewaries
{
    public class ValidateModelFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                throw new ValidationExceptionList("ModelState Is not Valid", errors);
            }

            await next();
        }
    }
}
public class ValidationExceptionList : Exception
{
    public new string? Message { get; }
    public List<string> Errors { get; }

    public ValidationExceptionList(List<string> errors)
    {
        Errors = errors;
    }
    public ValidationExceptionList(string message, List<string> errors)
    {
        Message = message;
        Errors = errors;
    }
}