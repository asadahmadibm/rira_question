using FluentValidation;
using GrpcCrudExample.Models;

namespace GrpcCrudExample.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.NationalCode).NotEmpty().Length(10);
            RuleFor(p => p.BirthDate).NotEmpty().LessThan(DateTime.Now);
        }
    }
}