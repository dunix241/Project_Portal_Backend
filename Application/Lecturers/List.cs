using Application.Core;
using Application.Lecturers.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Lecturers
{
    public class List
    {
        public class Query : IRequest<Result<ListLecturerResponseDto>>
        {
            public PagingParams QueryParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListLecturerResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ListLecturerResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturers = _context.Lecturers
                    .Where(s => s.IsActive)
                    .Include(s => s.School)
                    .Where(s => s.School.IsActive)
                    .AsQueryable();

               
                var responseDtoList = lecturers.Select(lecturer => _mapper.Map<GetLecturerResponseDto>(lecturer));
                var lectureResponseDto = new ListLecturerResponseDto();

                await lectureResponseDto.GetItemsAsync(responseDtoList.AsQueryable(), request.QueryParams.PageNumber, request.QueryParams.PageSize);
                return Result<ListLecturerResponseDto>.Success(lectureResponseDto);
            }
        }
    }
}
