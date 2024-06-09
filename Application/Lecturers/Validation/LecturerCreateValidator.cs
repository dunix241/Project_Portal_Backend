using Application.Lecturers.DTOs;
using FluentValidation;

namespace Application.Lecturers.Validation
{
    public class LecturerCreateValidator : AbstractValidator<CreateLecturerRequestDto>
    {
        public LecturerCreateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name cannot be empty.")
                .Must(BeValidName).WithMessage("First Name cannot contain numbers or special characters.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Last Name cannot be empty.")
                .Must(BeValidName).WithMessage("Last Name cannot contain numbers or special characters.");
            RuleFor(x => x.SchoolId).NotEmpty().WithMessage("School Id cannot be empty.");
        }

        private bool BeValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit) && !name.Any(char.IsPunctuation);
        }
    }
}
