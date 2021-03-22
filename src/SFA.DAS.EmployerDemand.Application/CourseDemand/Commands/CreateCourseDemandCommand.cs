using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommand : IRequest<Guid>
    {
        public Domain.Models.CourseDemand CourseDemand { get; set; } 
    }
}