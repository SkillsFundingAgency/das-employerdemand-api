using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemandByExpiredDemand
{
    public class GetCourseDemandByExpiredDemandQuery : IRequest<GetCourseDemandByExpiredDemandQueryResult>
    {
        public Guid ExpiredCourseDemandId { get ; set ; }
    }
}