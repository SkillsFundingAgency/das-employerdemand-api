using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class AggregatedCourseDemandSummary
    {
        public Guid Id { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public string CourseRoute { get; set; }
        public int EmployersCount { get; set; }
        public int ApprenticesCount { get; set; }
        public double DistanceInMiles { get; set; }
    }
}