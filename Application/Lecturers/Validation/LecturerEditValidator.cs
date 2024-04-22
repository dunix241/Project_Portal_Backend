using Application.Lecturers.DTOs;
using FluentValidation;

namespace Application.Lecturers.Validation
{
    public class LecturerEditValidator : AbstractValidator<EditLecturerRequestDto>
    {
        public LecturerEditValidator()
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
