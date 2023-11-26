using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Student;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Students
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateStudentRequestDto Student { get; set; }
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
                var student = _mapper.Map<Student>(request.Student);
                var school = await _context.Schools.FindAsync(request.Student.SchoolId);

                if (school == null)
                {
                    return Result<Unit>.Failure("School not found.");
                }
                student.School = school;

                _context.Students.Add(student);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
