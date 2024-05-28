using Application.Students.DTOs;
using FluentValidation;

namespace Application.Students.Validation
{
    public class StudentCreateValidator : AbstractValidator<CreateStudentRequestDto>
    {
        public StudentCreateValidator()
        {
            RuleFor(x => x.FirstName)
                 .NotEmpty().WithMessage("First Name cannot be empty.")
                 .Must(BeValidName).WithMessage("First Name cannot contain numbers or special characters.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Last Name cannot be empty.")
                .Must(BeValidName).WithMessage("Last Name cannot contain numbers or special characters.");
        }

        private bool BeValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit) && !name.Any(char.IsPunctuation);
        }
    }
}
