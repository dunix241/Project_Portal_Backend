using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs.Accounts.ValidationAttributes;

public class IsEmailUnique : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userManager = (UserManager<User>) validationContext.GetService(typeof(UserManager<User>))!;
        
        var propertyName = validationContext.MemberName;
        
        var exists = userManager.Users.Any(user => user.Email == (string)value);
        
        if (exists) return new ValidationResult("This "+ propertyName + " already exists");
        
        return ValidationResult.Success;
    }
}