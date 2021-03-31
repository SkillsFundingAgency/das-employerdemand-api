namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class AggregatedCourseDemandSummary
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public string CourseRoute { get; set; }
        public int EmployersCount { get; set; }
        public int ApprenticesCount { get; set; }
    }
}