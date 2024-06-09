using Application.Core;
using Application.Enrollments.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollment;

public class Get
{
    public class Query : IRequest<Result<GetEnrollmentResponseDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<GetEnrollmentResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<GetEnrollmentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var enrollment = await _dataContext.Enrollments
                    .Include(entity => entity.EnrollmentMembers)
                    .Include(entity => entity.ProjectSemester)
                    .Where(entity => entity.Id == request.Id)
                    .FirstOrDefaultAsync()
                ;
            if (enrollment == null)
            {
                return null;
            }

            var payload = _mapper.Map<GetEnrollmentResponseDto>(enrollment);

            return Result<GetEnrollmentResponseDto>.Success(payload);
        }
    }
}