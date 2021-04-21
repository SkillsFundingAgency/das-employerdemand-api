using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList
{
    public class GetEmployerCourseDemandListQuery : IRequest<GetEmployerCourseDemandListResult>
    {
        public int Ukprn { get ; set ; }
        public int CourseId { get ; set ; }
        public double? Lat { get ; set ; }
        public double? Lon { get ; set ; }
        public int? Radius { get ; set ; }
    }
}