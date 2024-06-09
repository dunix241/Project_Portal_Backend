using Application.Core;
using Application.Enrollments.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Enrollment;

public class EditEnrollmentMember
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public EditEnrollmentMemberRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var member = await _dataContext.EnrollmentMembers.FindAsync(request.Id);
            
            if (member == null) return null;

            if (member.IsAccepted != null)
            {
                return Result<Unit>.Failure("You already " + (member.IsAccepted.Value ? "accepted" : "rejected") + " This invitation");
            }

            _mapper.Map(request.Payload, member);
            await _dataContext.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}