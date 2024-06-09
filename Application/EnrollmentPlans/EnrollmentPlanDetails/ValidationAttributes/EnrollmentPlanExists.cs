using System.ComponentModel.DataAnnotations;
using Persistence;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails.ValidationAttributes;

public class EnrollmentPlanExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dataContext = (DataContext) validationContext.GetService(typeof(DataContext))!;
        
        var propertyName = validationContext.MemberName;
        
        var exists = dataContext.EnrollmentPlans.Any(entity => entity.Id == (Guid)value);
        
        if (exists) return new ValidationResult("Enrollment plan with id " + propertyName + " doesn't exist");
        
        return ValidationResult.Success;
    }
}
