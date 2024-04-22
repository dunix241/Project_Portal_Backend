using Application.Students.DTOs;
using FluentValidation;

namespace Application.Students.Validation
{
    public class StudentEditValidator : AbstractValidator<EditStudentRequestDto>
    {
        public StudentEditValidator()
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
