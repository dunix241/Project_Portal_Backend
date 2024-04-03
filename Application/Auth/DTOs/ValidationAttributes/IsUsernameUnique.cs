using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs.Accounts.ValidationAttributes;

public class IsUsernameUnique : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userManager = (UserManager<User>) validationContext.GetService(typeof(UserManager<User>))!;
        
        var propertyName = validationContext.MemberName;
        
        var exists = userManager.Users.Any(user => user.UserName == (string)value);
        
        if (exists) return new ValidationResult("This "+ propertyName + " already exists");
        
        return ValidationResult.Success;
    }
}