using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;

public class CreateEmployeeRequest
{
    //[Required(AllowEmptyStrings = false)]  since we are using fluent validation
    public string? FirstName { get; set; }
    //[Required(AllowEmptyStrings = false)]
    public string? LastName { get; set; }
    public string? SocialSecurityNumber { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class GetEmployeeResponse
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public required List<GetEmployeeResponseEmployeeBenefit> Benefits { get; set; }
}

public class GetEmployeeResponseEmployeeBenefit
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public BenefitType BenefitType { get; set; }
    public decimal Cost { get; set; }
}

public class UpdateEmployeeRequest
{
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
        .Must(name => !int.TryParse(name, out _)).WithMessage("First name cannot be a number.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
         .Must(name => !int.TryParse(name, out _)).WithMessage("Last name cannot be a number.");;
    }
}