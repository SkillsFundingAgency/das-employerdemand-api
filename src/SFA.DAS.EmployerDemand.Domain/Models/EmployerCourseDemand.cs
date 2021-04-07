using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class EmployerCourseDemand
    {
        public int ApprenticesCount { get ; set ; }
        public double Long { get ; set ; }
        public double Lat { get ; set ; }
        public string LocationName { get ; set ; }
        public Guid Id { get ; set ; }
        
        public static implicit operator EmployerCourseDemand(Entities.AggregatedCourseDemandSummary source)
        {
            return new EmployerCourseDemand
            {
                Id = source.Id.Value,
                LocationName = source.LocationName,
                Lat = source.Lat.Value,
                Long = source.Long.Value,
                ApprenticesCount = source.ApprenticesCount
            };
        }

        
    }
}