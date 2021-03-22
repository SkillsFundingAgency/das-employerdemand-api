using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommand : IRequest<Unit>
    {
        public Domain.Models.CourseDemand CourseDemand { get; set; } 
    }
}