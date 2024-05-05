using API.DTOs.Accounts;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth;

public class ChangePassword
{
    public class Command : IRequest<Result<Unit>>
    {
        public ChangePasswordRequestDTO RequestDto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _userAccessor;

        public Handler(UserManager<User> userManager, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var creds = _userAccessor.GetUser();
            if (creds == null)
            {
                return Result<Unit>.Failure("This action require login credentials");
            }
            
            var user = await _userManager.FindByEmailAsync(creds.Email);
            var result = await _userManager.ChangePasswordAsync(user, request.RequestDto.CurrentPassword, request.RequestDto.NewPassword);
            return result.Succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error changing your password");
        }
    }
}