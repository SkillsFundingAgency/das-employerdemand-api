using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand
{
    public class PatchCourseDemandCommand : IRequest<PatchCourseDemandCommandResponse>
    {
        public Guid Id { get; set; }
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public bool Stopped { get; set; }
    }
}