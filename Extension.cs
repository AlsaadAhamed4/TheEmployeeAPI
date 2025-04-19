using System;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheEmployeeAPI;
// this is adding validation msg in a structure
public static class Extensions
{

    public static ModelStateDictionary ToModelStateDictionary(this ValidationResult validationResult)  // this will say this field is valid and this not 
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in validationResult.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return modelState;
    }


    //another method for validation error.
    // public static ValidationProblemDetails ToValidationProblemDetails(this List<ValidationResult> validationResults)
    // {
    //     var problemDetails = new ValidationProblemDetails();

    //     foreach (var validationResult in validationResults)
    //     {
    //         foreach (var memberName in validationResult.MemberNames)
    //         {
    //             if (problemDetails.Errors.ContainsKey(memberName))
    //             {
    //                 problemDetails.Errors[memberName] = problemDetails.Errors[memberName].Concat([validationResult.ErrorMessage]).ToArray()!;
    //             }
    //             else
    //             {
    //                 problemDetails.Errors[memberName] = new List<string> { validationResult.ErrorMessage! }.ToArray();
    //             }
    //         }
    //     }

    //     return problemDetails;
    // }
}