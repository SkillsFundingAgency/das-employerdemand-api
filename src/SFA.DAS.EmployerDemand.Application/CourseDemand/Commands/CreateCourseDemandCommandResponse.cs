using System;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommandResponse
    {
        public Guid Id { get; set; }
        public bool IsCreated { get; set; }
    }
}