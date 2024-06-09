namespace Application.Projects.DTOs;

public class ListBasedOnEnrollmentPlanResponseDto
{
    private List<List<RegistrableProjectResponseDto>>? _list;
    public List<List<RegistrableProjectResponseDto>> Projects
    {
        get => (_list ??= new List<List<RegistrableProjectResponseDto>>());
        set => _list = value;
    }

    public class RegistrableProjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Registrable { get; set; }
        public Domain.Enrollment.Enrollment Enrollment { get; set; }
    }
}
