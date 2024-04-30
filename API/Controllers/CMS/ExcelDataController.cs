using Application.Core;
using Application.ExcelData;
using Application.Students;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CMS
{
    public class ExcelDataController : CmsApiController
    {
        [HttpGet("GetTemplate")]
        public async Task<IActionResult> ExportExcel()
        {
            var query = new GetTemplate.Query { };
            var result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                var excelStream = result.Value.ExcelStream;
                var file = File((byte[])excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Student" + ".xlsx");
                return file;
            }
            else
            {
                return HandleResult(result);
            }
        }
    }
}
