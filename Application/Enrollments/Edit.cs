using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Enrollments.DTOs;

public class Edit
{
    public class Command : IRequest<Result<Domain.Enrollment.Enrollment>>
    {
        public Guid Id { get; set; }
        public EditEnrollmentRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Domain.Enrollment.Enrollment>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<Domain.Enrollment.Enrollment>> Handle(Command request, CancellationToken cancellationToken)
        {
            var enrollment = await _dataContext.Enrollments.FindAsync(request.Id);
            
            if (enrollment == null)
            {
                return null;
            }
            if (enrollment.IsPublished)
            {
                return Result<Domain.Enrollment.Enrollment>.Failure("Can not edit an completed enrollment");
            }

            _mapper.Map(request.Payload, enrollment);
            await _dataContext.SaveChangesAsync();

            return Result<Domain.Enrollment.Enrollment>.Success(enrollment);
        }
    }
}