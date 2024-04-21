using Application.Core;
using Application.Lecturers.DTOs;
using Application.Students.DTOs;
using Application.Students.Validation;
using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Student;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Students
{
    public class ExcelImport
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
                using (var workbook = new XLWorkbook(request.ExcelStream))
                {
                    var worksheet = workbook.Worksheet(1);

                    var idString = worksheet.Cell(3, 3).GetValue<string>();
                    var schoolId = new Guid(idString); 
                    var school = await _context.Schools.FindAsync(schoolId);

                    if (school == null)
                    {
                        return Result<Unit>.Failure("School not found.");
                    }

                    var studentList = new List<CreateStudentRequestDto>();
                    for (int row = 6; row <= worksheet.LastRowUsed().RowNumber(); row++)
                    {
                        var timeString = worksheet.Cell(row, 3).GetValue<string>();
                        var studentDto = new CreateStudentRequestDto
                        {
                            Name = worksheet.Cell(row, 2).GetValue<string>(),                          
                            Email = worksheet.Cell(row, 3).GetValue<string>(),
                            PhoneNumber = worksheet.Cell(row, 4).GetValue<string>(),
                            IsActive = true,
                            SchoolId = schoolId,                          
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

                return Result<Unit>.Success(Unit.Value);
            }


        }
    }
}
