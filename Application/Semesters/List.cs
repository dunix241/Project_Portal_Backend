using Application.Core;
using Application.Semesters.DTOs;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Semesters
{
    public class List
    {
        public class Query : IRequest<Result<ListSemesterResponseDto>>
        {
            public PagingParams QueryParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListSemesterResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ListSemesterResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var semesters = _context.Semesters
                    .AsQueryable();

                var response = new ListSemesterResponseDto();

                await response.GetItemsAsync(semesters, request.QueryParams.PageNumber, request.QueryParams.PageSize);
                return Result<ListSemesterResponseDto>.Success(response);
            }
        }
    }
}
