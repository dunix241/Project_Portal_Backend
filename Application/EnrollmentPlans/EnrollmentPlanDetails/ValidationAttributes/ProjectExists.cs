using System.ComponentModel.DataAnnotations;
using Persistence;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails.ValidationAttributes;

public class ProjectExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dataContext = (DataContext) validationContext.GetService(typeof(DataContext))!;
        
        var propertyName = validationContext.MemberName;
        
        var exists = dataContext.Projects.Any(entity => entity.Id == (Guid)value);
        
        if (exists) return new ValidationResult("Project with id " + propertyName + " doesn't exist");
        
        return ValidationResult.Success;
    }
}
