using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetAggregatedCourseDemandSummaryResponse
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public int EmployersCount { get; set; }
        public int ApprenticesCount { get; set; }

        public static implicit operator GetAggregatedCourseDemandSummaryResponse(AggregatedCourseDemandSummary source)
        {
            return new GetAggregatedCourseDemandSummaryResponse
            {
                CourseId = source.CourseId,
                CourseTitle = source.CourseTitle,
                CourseLevel = source.CourseLevel,
                EmployersCount = source.EmployersCount,
                ApprenticesCount = source.ApprenticesCount
            };
        }
    }
}