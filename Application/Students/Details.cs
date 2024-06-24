using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class Details
    {
        public class Query : IRequest<Result<GetStudentResponseDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetStudentResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetStudentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context.Students
                    .Include(s => s.School)
                    .Where(s => s.UserId == request.Id)
                    .FirstOrDefaultAsync();

                if (student == null)
                {
                    Result<GetStudentResponseDto>.Failure("Not found");
                }

                var responseDto = _mapper.Map<GetStudentResponseDto>(student);
             
                var user = await _context.Users.FindAsync(student?.UserId);
                // responseDto = _mapper.Map<GetStudentResponseDto>(user);
                if (user != null)
                {
                    responseDto.Email = user.Email;
                    responseDto.IsActive = user.IsActive;
                    responseDto.Id = user.Id;
                    responseDto.FirstName = user.FirstName;
                    responseDto.LastName = user.LastName;
                    responseDto.FullName = user.FullName;
                    responseDto.PhoneNumber = user.PhoneNumber;
                    responseDto.AvatarId = user.AvatarId;
                    
                }

                return Result<GetStudentResponseDto>.Success(responseDto);
            }
        }
    }
}
