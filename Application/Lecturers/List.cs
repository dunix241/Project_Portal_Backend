using Application.Core;
using Application.Lecturers.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Result<ListLecturerResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturers = _context.Lecturers
                    .Include(s => s.School)
                    .Where(s => s.School.IsActive)
                    .AsQueryable();
               
                var responseDtoList = lecturers.Select(lecturer => _mapper.Map(
                    lecturer,
                      //_mapper.Map<GetLecturerResponseDto>(_context.Users.Where(entity => entity.Id == lecturer.UserId && entity.IsActive).FirstOrDefault()))
                      _mapper.Map<GetLecturerResponseDto>(_context.Users.Where(entity => entity.Id == lecturer.UserId).FirstOrDefault()))
                );
                var lectureResponseDto = new ListLecturerResponseDto();

                await lectureResponseDto.GetItemsAsync(responseDtoList.AsQueryable(), request.QueryParams.PageNumber, request.QueryParams.PageSize);
                return Result<ListLecturerResponseDto>.Success(lectureResponseDto);
            }
        }
    }
}
