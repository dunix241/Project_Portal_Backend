using Application.Authorization.Users;
using Application.Core;
using Application.Enrollment;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Members;

public class DeleteEnrollmentMember
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext dataContext, IAuthorizationService authorizationService, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _authorizationService = authorizationService;
            _userAccessor = userAccessor;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var enrollmentMember = await _dataContext.EnrollmentMembers
                .Include(entity => entity.Enrollment)
                .Where(entity => entity.Id == request.Id)
                .FirstOrDefaultAsync()
                ;

            if (enrollmentMember == null) return null;

            var isAuthorized = (
                    await _authorizationService
                        .AuthorizeAsync(_userAccessor.GetUser().User, enrollmentMember.Enrollment, UserOperations.ResetPassword)
                    ).Succeeded;
            
            if (!isAuthorized)
            {
                return Result<Unit>.Failure(Status.Forbid, "Unauthorized");
            }
            
            _dataContext.Remove(enrollmentMember);
            var succeed = await _dataContext.SaveChangesAsync() != 0;

            return succeed ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("");
        }
    }
    
}