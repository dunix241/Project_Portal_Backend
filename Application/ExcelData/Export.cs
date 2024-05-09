//using Application.Core;
//using Application.Minio.DTOs;
//using Application.Students.DTOs;
//using AutoMapper;
//using MediatR;
//using OfficeOpenXml;
//using Persistence;


//namespace Application.ExcelData
//{
//    public class Export
//    {
//        public class Query : IRequest<Result<GetExcelExportDto>>
//        {
//            public PagingParams ExportQueryParams { get; set; }
//            public SourceOwnerType SourceOwnerType { get; set; }
//        }

//        public class Handler : IRequestHandler<Query, Result<GetExcelExportDto>>
//        {
//            private readonly DataContext _context;
//            private readonly IMapper _mapper;
//            private readonly IMediator _mediator;

//            public Handler(DataContext context, IMapper mapper, IMediator mediator)
//            {
//                _context = context;
//                _mapper = mapper;
//                _mediator = mediator;
//            }

//            public async Task<Result<GetExcelExportDto>> Handle(Query request, CancellationToken cancellationToken)
//            {
//                var dataList = new List<object>();

//                if (request.SourceOwnerType == SourceOwnerType.Lecturer)
//                {
//                    var query = await _mediator.Send(new Application.Lecturers.List.Query { QueryParams = request.ExportQueryParams });
//                    if (query.IsSuccess)
//                    {
//                        dataList = query.Value.Items.ToList();
//                    }
                 
//                }
//                else
//                {
//                    var query = await _mediator.Send(new Application.Students.List.Query { QueryParams = request.ExportQueryParams });
//                    dataList = query.Value.Items.ToList();
//                }


//                using (var package = new ExcelPackage())
//                {
//                    var worksheet = package.Workbook.Worksheets.Add("Danh sách");

//                    worksheet.Cells["A1:H1"].Merge = true;
//                    worksheet.Cells["A1"].Value = "DANH SÁCH SINH VIÊN";
//                    worksheet.Cells["A1"].Style.Font.Size = 12;
//                    worksheet.Cells["A1"].Style.Font.Bold = true;
//                    worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

//                    worksheet.Cells["A2:H2"].Merge = true;
//                    worksheet.Cells["A2"].Value = "Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//                    worksheet.Cells["A2"].Style.Font.Size = 12;
//                    worksheet.Cells["A2"].Style.Font.Bold = true;
//                    worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

//                    var headers = new List<string> { "STT", "IRN", "Họ và tên", "School", "Email", "Phone" };
//                    for (int i = 0; i < headers.Count; i++)
//                    {
//                        worksheet.Cells[4, i + 1].Value = headers[i];
//                        worksheet.Cells[4, i + 1].Style.Font.Bold = true;
//                        worksheet.Cells[4, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
//                    }

//                    for (int i = 0; i < studentList.Count; i++)
//                    {
//                        var student = studentList[i];
//                        worksheet.Cells[i + 5, 1].Value = i + 1;
//                        worksheet.Cells[i + 5, 2].Value = student.IRN;
//                        worksheet.Cells[i + 5, 3].Value = student.FullName;
//                        worksheet.Cells[i + 5, 4].Value = student.SchoolName;
//                        worksheet.Cells[i + 5, 5].Value = student.Email;
//                        worksheet.Cells[i + 5, 6].Value = student.PhoneNumber;
//                    }

//                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

//                    using (var memoryStream = new MemoryStream())
//                    {
//                        package.SaveAs(memoryStream);
//                        return Result<GetExcelExportDto>.Success(new GetExcelExportDto { ExcelStream = memoryStream.ToArray() });
//                    }
//                }
//            }
//        }
//    }
//}
