using API.DTOs.Accounts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Auth;

public class Register
{
    public class Query : IRequest<User>
    {
        public RegisterRequestDTO RegisterRequestDto { get; set; }
    }

    public class Handler : IRequestHandler<Query, User>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediator _mediator;
        private readonly ILogger<Register> _logger;

        public Handler(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMediator mediator, ILogger<Register> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.RegisterRequestDto.UserName,
                Email = request.RegisterRequestDto.Email,
                Name = request.RegisterRequestDto.Name,
                Address = request.RegisterRequestDto.Address
            };

            var result = await _userManager.CreateAsync(user, request.RegisterRequestDto.Password);

            if (result.Succeeded)
            {
                _logger.LogError(user.Id);
                return user;
            }
            
            return null;
        }
    }
}
