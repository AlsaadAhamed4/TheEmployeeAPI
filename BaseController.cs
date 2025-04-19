using System;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace TheEmployeeAPI;

[ApiController]
[Route("[controller]")]  // this acts as mapGroup which tells start from /employees
[Produces("application/json")] // for swagger UI media type
public abstract class BaseController : Controller  // this is a abstract class which can be inherited by new class
{
    protected async Task<ValidationResult> ValidateAsync<T>(T instance)   // using fluent validator
    {
        var validator = HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
        {
            throw new ArgumentException($"No validator found for {typeof(T).Name}");
        }
        var validationContext = new ValidationContext<T>(instance);

        var result = await validator.ValidateAsync(validationContext);
        return result;
    }
}
