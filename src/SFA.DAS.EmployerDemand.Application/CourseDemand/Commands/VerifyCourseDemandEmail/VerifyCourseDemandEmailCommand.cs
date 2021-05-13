using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.VerifyCourseDemandEmail
{
    public class VerifyCourseDemandEmailCommand : IRequest<VerifyCourseDemandEmailCommandResponse>
    {
        public Guid Id { get ; set ; }
    }
}
