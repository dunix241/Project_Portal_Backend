using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth;

public class AddToRole
{
    public class Command : IRequest<Result<Unit>>
    {
        public string UserEmail { get; set; }
        public string Role { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public Handler(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var success = true;
            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                success &= await _roleManager.CreateAsync(new IdentityRole(request.Role)) != null;
            }

            if (success)
            {
                var user = await _userManager.FindByEmailAsync(request.UserEmail);
                success &= user != null;
                if (success)
                {
                    success &= await _userManager.AddToRoleAsync(user, request.Role) != null;
                }
            }

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error adding role");
        }
    }
}