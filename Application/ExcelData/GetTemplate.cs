using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Persistence;
using System.Linq;


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

                var schoolMap = _context.Schools
                    .ToDictionary(school => school.Name, school => school.Id.ToString());

                List<string> keyNameList = schoolMap.Keys.ToList();

                var typleList = new List<string> { "Lecturer", "Student" };


                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Danh sách");

                var schoolRange = ws.Range("A3", "A3");
                var userTypeRange = ws.Range("B3", "B3");

                schoolRange.SetDataValidation().List(string.Join(",", keyNameList), false);
                userTypeRange.SetDataValidation().List(string.Join(",", typleList), false);



                ws.Range("A1", "H1").Merge();
                ws.Cells("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cells("A1").Style.Font.FontSize = 12;
                ws.Cells("A1").Value = "DANH SÁCH DỮ DỮ LIỆU ĐẦU VÀO";
                ws.Cells("A1").Style.Font.Bold = true;
                ws.Range("A2", "H2").Merge();
                ws.Cells("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cells("A2").Style.Font.FontSize = 12;
                // ws.Cells("A2").Value = "Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                var listHeader = new List<string>
                    {
                        "A4", "B4", "C4", "D4", "E4",
                    };

                ws.Cells(listHeader[0]).Value = "STT";
                ws.Cells(listHeader[1]).Value = "First Name";
                ws.Cells(listHeader[2]).Value = "LastName";
                ws.Cells(listHeader[3]).Value = "Email";
                ws.Cells(listHeader[4]).Value = "Phone";

                listHeader.ForEach(c =>
                {
                    ws.Cells(c).Style.Font.Bold = true;
                    ws.Cell(c).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(c).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                });

                int row = 5;

                using (var ms = new MemoryStream())
                {
                    var stream = new MemoryStream();
                    wb.SaveAs(stream);
                    byte[] content = stream.ToArray();
                    return Result<GetExcelExportDto>.Success(new GetExcelExportDto { ExcelStream = content });
                }
            }
        }
    }
}
