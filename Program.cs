using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheEmployeeAPI;
using TheEmployeeAPI.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// in app database

// var employees = new List<Employee>
// {
//     new Employee { Id = 1, FirstName = "John", LastName = "Doe", SocialSecurityNumber ="3446" },
//     new Employee { Id = 2, FirstName = "Jane", LastName = "Doe", SocialSecurityNumber = "1234" }
// };

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TheEmployeeAPI.xml"));  // include xml document for swagger
});
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();   // registering a service (Repository created us by)
builder.Services.AddProblemDetails(); // service to prettify the error msg to json
builder.Services.AddValidatorsFromAssemblyContaining<Program>();  // for fluent validator
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>(); // adding the filter which we created to our controllers. (kind of micro-middleware)
}); // now we are going controller structure for the API

var app = builder.Build();

//var employeeRoute = app.MapGroup("employees"); // setting the group

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();  // put the controller in the middle for matching route

// employeeRoute.MapGet(string.Empty, (IRepository<Employee> repository) =>
// {
//     return Results.Ok(repository.GetAll().Select(employee => new GetEmployeeResponse
//     {
//         FirstName = employee.FirstName,
//         LastName = employee.LastName,
//         Address1 = employee.Address1,
//         Address2 = employee.Address2,
//         City = employee.City,
//         State = employee.State,
//         ZipCode = employee.ZipCode,
//         PhoneNumber = employee.PhoneNumber,
//         Email = employee.Email
//     }));
// });

// employeeRoute.MapGet("{id:int}", (int id, IRepository<Employee> repository) =>
// {
//     var employee = repository.GetById(id);
//     if (employee == null)
//     {
//         return Results.NotFound();
//     }

//     return Results.Ok(new GetEmployeeResponse
//     {
//         FirstName = employee.FirstName,
//         LastName = employee.LastName,
//         Address1 = employee.Address1,
//         Address2 = employee.Address2,
//         City = employee.City,
//         State = employee.State,
//         ZipCode = employee.ZipCode,
//         PhoneNumber = employee.PhoneNumber,
//         Email = employee.Email
//     });
// });

// employeeRoute.MapPost(string.Empty, async (CreateEmployeeRequest employeeRequest, IRepository<Employee> repository, IValidator<CreateEmployeeRequest> validator) =>
// {

//     var validationResults = await validator.ValidateAsync(employeeRequest);
//     if (!validationResults.IsValid)
//     {
//         return Results.ValidationProblem(validationResults.ToDictionary());
//     }
//     // var validationProblems = new List<ValidationResult>();  no need this as we are using fluent validator
//     // var isValid = Validator.TryValidateObject(employeeRequest, new ValidationContext(employeeRequest), validationProblems, true);
//     // if (!isValid)
//     // {
//     //     return Results.BadRequest(validationProblems.ToValidationProblemDetails());
//     // }
//     var newEmployee = new Employee
//     {
//         FirstName = employeeRequest.FirstName!,
//         LastName = employeeRequest.LastName!,
//         SocialSecurityNumber = employeeRequest.SocialSecurityNumber!,
//         Address1 = employeeRequest.Address1,
//         Address2 = employeeRequest.Address2,
//         City = employeeRequest.City,
//         State = employeeRequest.State,
//         ZipCode = employeeRequest.ZipCode,
//         PhoneNumber = employeeRequest.PhoneNumber,
//         Email = employeeRequest.Email
//     };
//     repository.Create(newEmployee);
//     return Results.Created($"/employees/{newEmployee.Id}", employeeRequest);
// });

// employeeRoute.MapPut("{id}", ([FromBody] UpdateEmployeeRequest employeeRequest, int id, [FromServices] IRepository<Employee> repository) =>
// {
//     var existingEmployee = repository.GetById(id);
//     if (existingEmployee == null)
//     {
//         return Results.NotFound();
//     }

//     existingEmployee.Address1 = employeeRequest.Address1;
//     existingEmployee.Address2 = employeeRequest.Address2;
//     existingEmployee.City = employeeRequest.City;
//     existingEmployee.State = employeeRequest.State;
//     existingEmployee.ZipCode = employeeRequest.ZipCode;
//     existingEmployee.PhoneNumber = employeeRequest.PhoneNumber;
//     existingEmployee.Email = employeeRequest.Email;

//     repository.Update(existingEmployee);
//     return Results.Ok(existingEmployee);
// });

app.Run();
