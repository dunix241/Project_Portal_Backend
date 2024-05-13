using Application.Core;
using Application.ExcelData;
using Application.Minio.DTOs;
using Application.Students;
using DocumentFormat.OpenXml.Bibliography;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS
{
    public class ExcelDataController : CmsApiController
    {
        [HttpGet("GetTemplate")]
        public async Task<IActionResult> GetTemplate()
        {
            var query = new GetTemplate.Query { };
            var result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                var excelStream = result.Value.ExcelStream;
                var file = File((byte[])excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportTemplate" + ".xlsx");
                return file;
            }
            else
            {
                return HandleResult(result);
            }
        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportExcel([FromQuery] PagingParams pagingParams, ExcelExportEnum type)
        {
            var query = new Export.Query { QueryParams = pagingParams, ExcelExportEnum = type  };
            var result = await Mediator.Send(query);
            var fileName = type.ToString();

            if (result.IsSuccess)
            {
                var excelStream = result.Value.ExcelStream;
                var file = File((byte[])excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
                return file;
            }
            else
            {
                return HandleResult(result);
            }
        }
        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var result = await Mediator.Send(new Import.Command { ExcelStream = file.OpenReadStream() });
            return HandleResult(result);
        }
    }
}
