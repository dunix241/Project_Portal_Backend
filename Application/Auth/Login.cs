using API.DTOs.Accounts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Auth;

public class Login
{
    public class Query : IRequest<User>
    {
        public LoginRequestDTO LoginRequestDto { get; set; }
    }

    public class Handler : IRequestHandler<Query, User>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Handler(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginRequestDto.Email);

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginRequestDto.Password, false);

            if (result.Succeeded)
            {
                user = await _context
                    .Users
                    .Include(usr => usr.Avatar)
                    .FirstOrDefaultAsync(usr => usr.Id == user.Id);
                
                return user;
            }

            return null;
        }
    }
}
