using Application.Students.DTOs;
using FluentValidation;

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
