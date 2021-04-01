using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class AggregatedCourseDemandSummary
    {
        public Guid Id { get ; set ; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public string CourseRoute { get; set; }
        public int EmployersCount { get; set; }
        public int ApprenticesCount { get; set; }


        public static implicit operator AggregatedCourseDemandSummary(Entities.AggregatedCourseDemandSummary source)
        {
            return new AggregatedCourseDemandSummary
            {
                Id = source.Id,
                CourseId = source.CourseId,
                CourseTitle = source.CourseTitle,
                CourseLevel = source.CourseLevel,
                CourseRoute = source.CourseRoute,
                EmployersCount = source.EmployersCount,
                ApprenticesCount = source.ApprenticesCount
            };
        }

        public AggregatedCourseDemandSummary ()
        {
        }
        
        public AggregatedCourseDemandSummary (
            int courseId,
            string courseTitle,
            int courseLevel,
            string courseRoute,
            IReadOnlyCollection<Entities.AggregatedCourseDemandSummary> list)
        {
            CourseId = courseId;
            CourseLevel = courseLevel;
            CourseRoute = courseRoute;
            CourseTitle = courseTitle;
            ApprenticesCount = list.Sum(c => c.ApprenticesCount);
            EmployersCount = list.Count;
        }
    }
}
