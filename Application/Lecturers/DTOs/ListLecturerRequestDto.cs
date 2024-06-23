using Application.Core;

namespace Application.Lecturers.DTOs
{
    public class ListLecturerRequestDto: PagingParams
    {
        public Guid? SchoolId { get; set; }

    }
}
