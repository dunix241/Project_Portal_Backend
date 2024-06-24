

using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using Application.Users.DTOs;
using AutoMapper;
using Domain;
using Domain.Semester;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Users
{
    public class EditUser
    {
        public class Command : IRequest<Result<User>>
        {
            public string Id { get; set; }
            public EditUserRequest request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<User>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;

            }

            public async Task<Result<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = _context.Users.Where( x=> x.Id == request.Id).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                _mapper.Map(request.request, user);
                await _context.SaveChangesAsync();

                return Result<User>.Success(user);

            }
        }
    }
}