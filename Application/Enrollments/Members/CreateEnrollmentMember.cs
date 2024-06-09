using System.ComponentModel.DataAnnotations;
using Application.Core;
using Application.Enrollments.DTOs;
using Domain;
using Domain.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments.Members;

public class CreateEnrollmentMember
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid EnrollmentId { get; set; }
        public CreateEnrollmentMemberRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        public Handler(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var userExists = (await _userManager.FindByEmailAsync(request.Payload.Email)) != null;
            if (!userExists)
            {
                return Result<Unit>.Failure($"User with email `{request.Payload.Email}` cannot be found");
            }

            var enrollment = await _dataContext.Enrollments.FindAsync(request.EnrollmentId);
            if (enrollment == null) return null;

            var hasAlreadyEnrolled = await _dataContext.EnrollmentMembers
                    .Where(entity => entity.Email == request.Payload.Email)
                    .Include(entity => entity.Enrollment)
                    .Where(entity =>
                        entity.Enrollment.ProjectId == enrollment.ProjectId &&
                        entity.Enrollment.SemesterId == enrollment.SemesterId)
                    .AnyAsync()
                ;
            if (hasAlreadyEnrolled)
            {
                return Result<Unit>.Failure($"User with email `{request.Payload.Email}` already enrolled in another project");
            }

            var enrollmentMember = new EnrollmentMember
            {
                Email = request.Payload.Email, EnrollmentId = request.EnrollmentId
            };

            _dataContext.EnrollmentMembers.Add(enrollmentMember);

            var succeeded = await _dataContext.SaveChangesAsync() != 0;
            
            var result = succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("").SetId(Errors.ERROR_ADDING_ENROLLMENT_MEMBER);
            
            return succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("");
        }
    }
}