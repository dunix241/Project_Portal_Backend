using Application.Students.DTOs;
using Domain.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students
{
    public class StudentValidator : AbstractValidator<CreateStudentRequestDto>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name cannot be empty.")
             .Must(BeValidName).WithMessage("Name cannot contain numbers or special characters.");
        }

        private bool BeValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit) && !name.Any(char.IsPunctuation);
        }
    }
}
