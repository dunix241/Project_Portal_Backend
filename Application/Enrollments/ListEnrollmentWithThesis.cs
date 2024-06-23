using Application.Core;
using Application.Enrollments.DTOs;
using Application.Minio;
using Application.Minio.DTOs;
using AutoMapper;
using Domain.Submission;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Enrollments
{
    public class ListEnrollmentWithThesis
    {
        public class Query : IRequest<Result<ListEnrollmentWithThesisDto>>
        {
            public ListEnrollmentRequestDto Payload;
        }

        public class Handler : IRequestHandler<Query, Result<ListEnrollmentWithThesisDto>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(DataContext dataContext, IMapper mapper, IMediator mediator)
            {
                _dataContext = dataContext;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Result<ListEnrollmentWithThesisDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var payload = request.Payload;
                var query = _dataContext.Enrollments
                  .Where(x =>
                      (payload.SemesterId == null || x.SemesterId == payload.SemesterId) &&
                      (payload.UserId == null || x.OwnerId == payload.UserId) &&
                      (payload.IsPublished == null || x.IsPublished == payload.IsPublished) &&
                      (payload.SchoolId == null || payload.SchoolId == x.ProjectSemester.Project.SchoolId)
                  );
                var response = await query.Select(x => new EnrollmentWithThesis
                {
                    CanBeForked = x.CanBeForked,
                    Description = x.Description,
                    FileResponseDto = new Minio.DTOs.FileResponseDto { Id = x.ThesisId },
                    ForkedFromId = x.ForkedFromId,
                    Id = x.Id,
                    ForkFrom = x.ForkFrom,
                    HeirFortunes = x.HeirFortunes,
                    IsPublished = x.IsPublished,
                    OwnerId = x.OwnerId,
                    ProjectId = x.ProjectId,
                    PublishDate = x.PublishDate,
                    SemesterId = x.SemesterId,
                    ThesisId = x.ThesisId,
                    Title = x.Title,
                    Vision = x.Vision,
                    RegisterDate = x.RegisterDate,
                }).ToListAsync();

                foreach (var res in response)
                {
                    var fileDto = res.FileResponseDto;
                    var fileId = fileDto?.Id;
                    if (fileId != null)
                    {
                        var file = await _dataContext.Files.Where(s => s.Id == fileId).FirstOrDefaultAsync();
                        if (file != null)
                        {
                            var dto = await _mediator.Send(new GetFile.Query { Id = file.Id });
                            if (dto != null && dto.Value != null)
                            {
                                fileDto.Url = dto.Value.Url;
                                fileDto.Name = dto.Value.Name;
                            }
                        }
                    }
                    res.FileResponseDto = new FileResponseDto
                    { Id = fileDto?.Id, Name = fileDto?.Name, Url = fileDto?.Url };
                }

                var test = response.AsQueryable();

                var enrollmentPlans = new ListEnrollmentWithThesisDto();
                await enrollmentPlans.GetItemsAsync(test, payload.PageNumber, payload.PageSize);

                return Result<ListEnrollmentWithThesisDto>.Success(enrollmentPlans);
            }
        }
    }
}
