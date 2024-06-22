using Application.Core;
using Application.Enrollments.DTOs;
using Application.Enrollments.Members;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments;

public class Create
{
    public class Command : IRequest<Result<CreateEnrollmentResponseDto>>
    {
        public CreateEnrollmentRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<CreateEnrollmentResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;

        public Handler(DataContext dataContext, IMapper mapper, UserManager<User> userManager, IUserAccessor userAccessor, IMediator mediator)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _userManager = userManager;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }
        
        public async Task<Result<CreateEnrollmentResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var enrollment = _mapper.Map<Domain.Enrollment.Enrollment>(request.Payload);
                
            enrollment.OwnerId = _userAccessor.GetUser().Id;
            
            enrollment.SemesterId = (await _dataContext.Semesters.FirstOrDefaultAsync(entity => entity.StartRegistrationDate <= DateTime.Today && entity.EndRegistrationDate >= DateTime.Today))!.Id;
            if (enrollment.SemesterId == null)
            {
                return Result<CreateEnrollmentResponseDto>.Failure("Ouside the allowed registration time");
            }
            
            using var transaction = _dataContext.Database.BeginTransaction();
            
            _dataContext.Enrollments.Add(enrollment);
            
            var owner = new EnrollmentMember{EnrollmentId = enrollment.Id, IsAccepted = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, UserId = _userAccessor.GetUser().Id};
            _dataContext.EnrollmentMembers.Add(owner);
            
            var succeeded = (await _dataContext.SaveChangesAsync()) != 0;

            if (!succeeded) return Result<CreateEnrollmentResponseDto>.Failure("A problem occurred while we trying to create your enrollment");
                
            var messages = new List<string>();
            
            foreach (var email in request.Payload.Emails)
            {
                var result = await _mediator.Send(new CreateEnrollmentMember.Command
                {
                    EnrollmentId = enrollment.Id, Payload = new CreateEnrollmentMemberRequestDto{Email = email}
                });
                
                if (result.Id == Errors.ERROR_ADDING_ENROLLMENT_MEMBER)
                {
                    return Result<CreateEnrollmentResponseDto>.Failure("Cannot save enrollment members");
                }
                
                if (!result.IsSuccess)
                {
                    messages.Add(result.Error);
                }
            }
            
            await transaction.CommitAsync(cancellationToken);
            
            return Result<CreateEnrollmentResponseDto>.Success(new CreateEnrollmentResponseDto{Messages = messages});
        }
    }
}