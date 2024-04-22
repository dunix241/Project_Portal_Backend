using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class List
    {
        public class Query : IRequest<Result<ListStudentResponseDto>>
        {
            public PagingParams QueryParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListStudentResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ListStudentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var students =  _context.Students
                    .Where(s => s.IsActive)
                    .Include(s => s.School)
                    .Where(s => s.School.IsActive)
                    .AsQueryable();

                var responseDtoList = students.Select(student => _mapper.Map<GetStudentResponseDto>(student));
                var listStudentResponseDto = new ListStudentResponseDto();

                await listStudentResponseDto.GetItemsAsync(responseDtoList.AsQueryable(), request.QueryParams.PageNumber, request.QueryParams.PageSize);
                return Result<ListStudentResponseDto>.Success(listStudentResponseDto);
            }
        }
    }
}