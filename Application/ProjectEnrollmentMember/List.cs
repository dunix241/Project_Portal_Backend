using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectEnrollmentMember.DTOs;
using Application.Schools.DTOs;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectEnrollmentMember
{
    public class List
    {
        public class Query : IRequest<Result<ListProjectEnrollmentMemberDtoResponseDto>>
        {
            public PagingParams Pagination { get; set; }
            public LidtProjectErollmentMemberRequestDto Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListProjectEnrollmentMemberDtoResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListProjectEnrollmentMemberDtoResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.ProjectEnrollmentMembers
                     .Where(m => m.ProjectEnrollmentId == request.Filter.EnrollmentID).AsQueryable();

                var members = new ListProjectEnrollmentMemberDtoResponseDto();
                await members.GetItemsAsync(query, request.Pagination.PageNumber, request.Pagination.PageSize);

                return Result<ListProjectEnrollmentMemberDtoResponseDto>.Success(members);
            }
        }
    }
}
