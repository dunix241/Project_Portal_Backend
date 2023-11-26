using Application.Core;
using Application.Lecturers.DTOs;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lecturers
{
    public class Details
    {
        public class Query : IRequest<Result<GetLecturerResponseDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetLecturerResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetLecturerResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturer = await _context.Lecturers
                   .Where(s => s.IsActive)
                   .Include(s => s.School)
                   .Where(s => s.Id == request.Id && s.School.IsActive)
                   .FirstOrDefaultAsync();


                if (lecturer == null)
                {
                    return Result<GetLecturerResponseDto>.Failure("Lecturer not found.");
                }

                var responseDto = GetLecturerResponseDto.FromLecturer(lecturer);

                return Result<GetLecturerResponseDto>.Success(responseDto);
            }
        }
    }
}
