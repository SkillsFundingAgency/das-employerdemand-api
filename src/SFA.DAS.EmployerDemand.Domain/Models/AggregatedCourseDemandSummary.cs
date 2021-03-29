namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class AggregatedCourseDemandSummary
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public int EmployersCount { get; set; }
        public int ApprenticesCount { get; set; }

        public static implicit operator AggregatedCourseDemandSummary(Entities.AggregatedCourseDemandSummary source)
        {
            return new AggregatedCourseDemandSummary
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
