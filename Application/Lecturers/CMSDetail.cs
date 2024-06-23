using Application.Core;
using Application.Lecturers.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Lecturers
{
    public class CMSDetail
    {
        public class Query : IRequest<Result<CMGetLectureResponseDto>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CMGetLectureResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CMGetLectureResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturer = await _context.Lecturers
                  .Where(s => s.UserId == request.Id)
                  .Include(x => x.School)
                  .FirstOrDefaultAsync();

                if (lecturer == null)
                {
                    return Result<CMGetLectureResponseDto>.Failure("Lecturer not found.");
                }
                if (lecturer?.SchoolId != null)
                {
                    var school = _context.Schools.FindAsync(lecturer.SchoolId);
                    if (school == null)
                    {
                        return Result<CMGetLectureResponseDto>.Failure("School not found.");
                    }
                }

                var user = await _context.Users.FindAsync(lecturer.UserId);

                var responseDto = _mapper.Map<CMGetLectureResponseDto>(_context.Users.FirstOrDefault(entity => entity.Id == lecturer.UserId));
                responseDto.SchoolId = lecturer?.SchoolId;
                responseDto.SchoolName = lecturer?.School?.Name;
                responseDto.Headline = lecturer?.Headline;
                responseDto.Title = lecturer?.Title;

                if(responseDto.contactViaEmail == false)
                {
                    responseDto.Email = null;
                }
                if(responseDto.contactViaPhoneNumber == false)
                {
                    responseDto.PhoneNumber = null;
                }

                return Result<CMGetLectureResponseDto>.Success(responseDto);
            }
        }
    }
}
