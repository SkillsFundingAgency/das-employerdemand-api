using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommand : IRequest<CreateCourseDemandCommandResponse>
    {
        public Domain.Models.CourseDemand CourseDemand { get; set; } 
    }
}