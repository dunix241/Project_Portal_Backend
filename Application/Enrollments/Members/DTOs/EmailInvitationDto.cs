namespace Application.Enrollments.DTOs;

public class EmailInvitationDto
{
    public string EnrollmentTitle { get; set; }
    public string ProjectName { get; set; }
    public string InvitorName { get; set; }
    public string InvitorEmail { get; set; }
    public string Url { get; set; }
}