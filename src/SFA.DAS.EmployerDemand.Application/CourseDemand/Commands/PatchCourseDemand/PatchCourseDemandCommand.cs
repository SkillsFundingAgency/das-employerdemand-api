using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand
{
    public class PatchCourseDemandCommand : IRequest<PatchCourseDemandCommandResponse>
    {
        public Domain.Models.CourseDemand CourseDemand { get; set; }
    }
}