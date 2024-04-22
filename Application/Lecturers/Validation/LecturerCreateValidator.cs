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
    public class LecturerCreateValidator : AbstractValidator<CreateLecturerRequedtDto>
    {
        public LecturerCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .Must(BeValidName).WithMessage("Name cannot contain numbers or special characters.");
            RuleFor(x => x.SchoolId).NotEmpty().WithMessage("School Id cannot be empty.");
        }

        private bool BeValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit) && !name.Any(char.IsPunctuation);
        }
    }
}
