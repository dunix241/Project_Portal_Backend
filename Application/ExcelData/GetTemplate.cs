using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using MediatR;
using OfficeOpenXml;
using Persistence;

namespace Application.ExcelData
{
    public class GetTemplate
    {
        public class Query : IRequest<Result<GetExcelExportDto>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<GetExcelExportDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(DataContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Result<GetExcelExportDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schoolList = _context.Schools.Select(school => school.Name).ToList();
                var typleList = new List<string> { "Lecturer", "Student" };
                string defaultType = "Lecturer";
                string defaultSchool = schoolList.FirstOrDefault();

                using (var excelPackage = new ExcelPackage())
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("List");

                    worksheet.Cells["A1:H1"].Merge = true;
                    worksheet.Cells["A1"].Value = "INPUT DATA SHEET";
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["A1"].Style.Font.Size = 12;
                    worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells["A2:H2"].Merge = true;
                    worksheet.Cells["A2"].Value = "Release date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    worksheet.Cells["A2"].Style.Font.Size = 12;
                    worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells["A3"].Value = "School";
                    worksheet.Cells["A3"].Style.Font.Bold = true;
                    var schoolRange = worksheet.Cells["B3"];
                    schoolRange.DataValidation.AddListDataValidation().Formula.Values.Add(string.Join(",", schoolList));
                    schoolRange.Value = defaultSchool;

                    worksheet.Cells["C3"].Value = "Data Type";
                    worksheet.Cells["C3"].Style.Font.Bold = true;
                    var userTypeRange = worksheet.Cells["D3"];
                    userTypeRange.DataValidation.AddListDataValidation().Formula.Values.Add(string.Join(",", typleList));
                    userTypeRange.Value = defaultType;

                    worksheet.Cells["A4"].Value = "Use drop down list to choose School and DataType";
                    worksheet.Cells["A4:H4"].Merge = true;
                    worksheet.Cells["A4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var listHeader = new List<string>
                    {
                        "A5", "B5", "C5", "D5", "E5"
                    };

                    worksheet.Cells[listHeader[0]].Value = "STT";
                    worksheet.Cells[listHeader[1]].Value = "First Name";
                    worksheet.Cells[listHeader[2]].Value = "LastName";
                    worksheet.Cells[listHeader[3]].Value = "Email";
                    worksheet.Cells[listHeader[4]].Value = "Phone";

                    foreach (var headerCell in listHeader)
                    {
                        var cell = worksheet.Cells[headerCell];
                        cell.Style.Font.Bold = true;
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    int row = 6;

                    using (var ms = new MemoryStream())
                    {
                        excelPackage.SaveAs(ms);
                        byte[] content = ms.ToArray();
                        return Result<GetExcelExportDto>.Success(new GetExcelExportDto { ExcelStream = content });
                    }
                }
            }
        }
    }
}
