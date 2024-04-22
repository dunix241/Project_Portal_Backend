using Application.Core;
using Application.Students.DTOs;
using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Persistence;

namespace Application.Students
{
    public class ExcelExport
    {
        public class Query : IRequest<Result<GetExcelExportDto>>
        {
            public PagingParams ExportQueryParams { get; set; }
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
                var query = await _mediator.Send(new List.Query { QueryParams = request.ExportQueryParams });
                var studentList = query.Value.Items.ToList();

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Danh sách");

                ws.Range("A1", "H1").Merge();         
                ws.Cells("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cells("A1").Style.Font.FontSize = 12;
                ws.Cells("A1").Value = "DANH SÁCH SINH VIÊN";
                ws.Cells("A1").Style.Font.Bold = true;
                ws.Range("A2", "H2").Merge();
                ws.Cells("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cells("A2").Style.Font.FontSize = 12;
                ws.Cells("A2").Value = "Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                var listHeader = new List<string>
                    {
                        "A4", "B4", "C4", "D4", "E4", "F4","G4"
                    };

                ws.Cells(listHeader[0]).Value = "STT";
                ws.Cells(listHeader[1]).Value = "Id";
                ws.Cells(listHeader[2]).Value = "Họ và tên";
                ws.Cells(listHeader[3]).Value = "School";
                ws.Cells(listHeader[4]).Value = "Email";
                ws.Cells(listHeader[5]).Value = "Phone";

                listHeader.ForEach(c =>
                {
                    ws.Cells(c).Style.Font.Bold = true;
                    ws.Cell(c).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(c).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                });

                int row = 5;

                if (studentList.Any())
                {
                    for (int i = 0; i < studentList.Count; i++)
                    {
                        ws.Cells("A" + row.ToString()).Value = i + 1;
                        ws.Cells("B" + row).Value = studentList[i].Id.ToString();
                        ws.Cells("C" + row).Value = studentList[i].Name.ToString();
                        ws.Cells("D" + row).Value = studentList[i].SchoolName.ToString();
                        ws.Cells("E" + row).Value = studentList[i].Email.ToString();
                        ws.Cells("F" + row).Value = studentList[i].PhoneNumber.ToString();
                        row++;
                    }
                    ws.Columns().AdjustToContents();
                }

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
