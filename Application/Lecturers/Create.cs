using Application.Core;
using Application.Lecturers.DTOs;
using Application.MockDomains.DTOs;
using AutoMapper;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.Student;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lecturers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateLecturerRequedtDto Lecturer { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var lecturer = _mapper.Map<Lecturer>(request.Lecturer);
                var school = await _context.Schools.FindAsync(request.Lecturer.SchoolId);

                if (school == null)
                {
                    return Result<Unit>.Failure("School not found.");
                }
                lecturer.School = school;

                _context.Lecturers.Add(lecturer);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
