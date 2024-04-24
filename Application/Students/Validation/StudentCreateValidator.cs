using Application.Students.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Validation
{
    public class StudentCreateValidator : AbstractValidator<CreateStudentRequestDto>
    {
        public StudentCreateValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("FirstName cannot be empty.")
             .Must(BeValidName).WithMessage("FirstName cannot contain numbers or special characters.");
        }

        private bool BeValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit) && !name.Any(char.IsPunctuation);
        }
    }
}
