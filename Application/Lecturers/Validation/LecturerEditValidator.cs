using Application.Lecturers.DTOs;
using Application.Students.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lecturers.Validation
{
    public class LecturerEditValidator : AbstractValidator<EditLecturerRequestDto>
    {
        public LecturerEditValidator()
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
