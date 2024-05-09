using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using Application.Students.DTOs;
using Application.Students.Validation;
using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Lecturer;
using Domain.Student;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Persistence;
using System.Text;


namespace Application.ExcelData
{
    public class Import
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Stream ExcelStream { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ExcelStream).NotNull().WithMessage("Excel file stream is required.");
            }
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
                using (var excelPackage = new ExcelPackage(request.ExcelStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[1];

                    var schoolName = worksheet.Cells["B3"].Text;
                    var sourceOwnerType = worksheet.Cells["D3"].Text;

                    var school = await _context.Schools.Where(x => x.Name == schoolName).FirstOrDefaultAsync();

                    if (school == null)
                    {
                        return Result<Unit>.Failure("School not found.");
                    }
                    if (sourceOwnerType.Equals("Lecturer", StringComparison.OrdinalIgnoreCase))
                    {
                        var lecturerList = new List<CreateLecturerRequedtDto>();
                        for (int row = 7; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var lecturerDto = new CreateLecturerRequedtDto
                            {
                                FirstName = worksheet.Cells[row, 2].Text,
                                LastName = worksheet.Cells[row, 3].Text,
                                Email = worksheet.Cells[row, 4].Text,
                                PhoneNumber = worksheet.Cells[row, 5].Text,
                                IsActive = true,
                                SchoolId = school.Id,

                            };

                            var validator = new LecturerCreateValidator();
                            var validateResult = await validator.ValidateAsync(lecturerDto);

                            if (!validateResult.IsValid)
                            {
                                var errorMessage = new StringBuilder();
                                errorMessage.AppendLine("Validation failed:");

                                foreach (var error in validateResult.Errors)
                                {
                                    errorMessage.AppendLine($"- {error.ErrorMessage}");
                                }

                                return Result<Unit>.Failure(errorMessage.ToString());
                            }

                            lecturerList.Add(lecturerDto);
                        }
                        var lecturerEntities = _mapper.Map<List<Lecturer>>(lecturerList);
                        _context.Lecturers.AddRange(lecturerEntities);

                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        var studentList = new List<CreateStudentRequestDto>();
                        for (int row = 7; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var timeString = worksheet.Cells[row, 3].Text;
                            var studentDto = new CreateStudentRequestDto
                            {
                                FirstName = worksheet.Cells[row, 2].Text,
                                LastName = worksheet.Cells[row, 3].Text,
                                Email = worksheet.Cells[row, 4].Text,
                                PhoneNumber = worksheet.Cells[row, 5].Text,
                                IsActive = true,
                                SchoolId = school.Id,
                            };

                            var validator = new StudentCreateValidator();
                            var validateResult = await validator.ValidateAsync(studentDto);

                            if (!validateResult.IsValid)
                            {
                                var errorMessage = new StringBuilder();
                                errorMessage.AppendLine("Validation failed:");

                                foreach (var error in validateResult.Errors)
                                {
                                    errorMessage.AppendLine($"- {error.ErrorMessage}");
                                }

                                return Result<Unit>.Failure(errorMessage.ToString());
                            }

                            studentList.Add(studentDto);
                        }

                        var studentEntities = _mapper.Map<List<Student>>(studentList);
                        _context.Students.AddRange(studentEntities);

                        await _context.SaveChangesAsync();
                    }
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
