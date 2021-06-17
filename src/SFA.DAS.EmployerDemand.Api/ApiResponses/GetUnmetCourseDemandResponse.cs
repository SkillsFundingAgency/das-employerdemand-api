using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetUnmetCourseDemandResponse
    {
        public List<GetUnmetCourseDemand> UnmetCourseDemands { get; set; }
    }
    
    
    public class GetUnmetCourseDemand
    {
        public Guid Id { get; set; }
        public int CourseId { get; set; }
        public static implicit operator GetUnmetCourseDemand(Domain.Models.CourseDemand source)
        {
            return new GetUnmetCourseDemand
            {
                Id = source.Id,
                CourseId = source.Course.Id
            };
        }
    }
}