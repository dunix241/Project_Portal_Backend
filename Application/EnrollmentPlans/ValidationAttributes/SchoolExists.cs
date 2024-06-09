using System.ComponentModel.DataAnnotations;
using Persistence;

namespace Application.EnrollmentPlans.ValidationAttributes;

public class SchoolExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dataContext = (DataContext) validationContext.GetService(typeof(DataContext))!;
        
        var propertyName = validationContext.MemberName;
        
        var exists = dataContext.Schools.Any(entity => entity.Id == (Guid)value);
        
        if (exists) return new ValidationResult("School with id " + propertyName + " doesn't exist");
        
        return ValidationResult.Success;
    }
}
