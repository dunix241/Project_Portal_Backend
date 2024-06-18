using Application.Core;
using Application.Lecturers.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Details
    {
        public class Query : IRequest<Result<User>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<User>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);
                if (user == null)
                {
                    return Result<User>.Failure("User not found.");
                }


                return Result<User>.Success(user);
            }
        }
    }
}
