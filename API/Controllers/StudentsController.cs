using Application.Core;
using Application.Students;
using Application.Students.DTOs;
using Domain.Lecturer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "List students")]
        public async Task<IActionResult> ListStudents([FromQuery] PagingParams pagingParams)
        {
            return HandleResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a student")]
        public async Task<IActionResult> GetStudent(string id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
        // [HttpGet("BySchoolId")]
        // public async Task<IActionResult> GetStudentBySchoolId(Guid id, [FromQuery] PagingParams pagingParams)
        // {
        //     return HandleResult(await Mediator.Send(new ListStudentBySchool.Query { Id = id, QueryParams = pagingParams }));
        // }
        [HttpPost()]
        public async Task<IActionResult> CreateAndAssignToSchool(CreateStudentRequestDto student)
        {

            var result = await Mediator.Send(new Create.Command { Student = student });
            return HandleResult(result);
        }



        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a student")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            return HandleResult(await Mediator.Send((new Delete.Command { Id = id })));
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var result = await Mediator.Send(new ExcelImport.Command { ExcelStream = file.OpenReadStream() });
            return HandleResult(result);
        }


        [HttpGet("Export-excel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PagingParams exportQueryParams)
        {
            var query = new ExcelExport.Query { ExportQueryParams = exportQueryParams };
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

