using System.Collections.Generic;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList
{
    public class GetAggregatedCourseDemandListQuery : IRequest<GetAggregatedCourseDemandListResult>
    {
        public int Ukprn { get; set; }
        public int? CourseId { get ; set ; }
        public double? Lat { get ; set ; }
        public double? Lon { get ; set ; }
        public int? Radius { get ; set ; }
        public IList<string> Routes { get; set; }
    }
}