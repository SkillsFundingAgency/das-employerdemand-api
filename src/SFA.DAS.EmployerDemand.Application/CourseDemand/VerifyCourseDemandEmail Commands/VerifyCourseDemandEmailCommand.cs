using MediatR;


namespace SFA.DAS.EmployerDemand.Application.CourseDemand.VerifyCourseDemandEmail_Commands
{
    public class VerifyCourseDemandEmailCommand : IRequest<VerifyCourseDemandEmailResponse>
    {
        public Domain.Models.CourseDemand CourseDemand { get; set; }
    }
}
