using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList
{
    public class GetAggregatedCourseDemandListQuery : IRequest<GetAggregatedCourseDemandListResult>
    {
        public int Ukprn { get; set; }
    }
}