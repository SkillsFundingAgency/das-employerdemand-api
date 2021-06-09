using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.StopCourseDemand
{
    public class StopCourseDemandCommand : IRequest<StopCourseDemandCommandResult>
    {
        public Guid Id { get; set; }
    }
}

