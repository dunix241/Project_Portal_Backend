using System.ComponentModel.DataAnnotations;

namespace Application.Enrollments.ValidationAttributes;

public class EmailAddresses : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        IList<string> list = value as IList<string>;
        
        EmailAddressAttribute emailAttribute = new EmailAddressAttribute();
        var success = (list != null && list.All(email => emailAttribute.IsValid(email)));
        
        var propertyName = validationContext.MemberName;
        if (success) return new ValidationResult($"Field `{propertyName}` contains some invalid email addresses");
        
        return ValidationResult.Success;
    }
}