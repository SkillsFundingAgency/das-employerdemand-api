using System;
using System.Text.RegularExpressions;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class EmployerCourseDemand
    {
        private const string PostcodeRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s*\d[A-Za-z]{2}\s*";
        private const string OutcodeDistrictRegex = @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s[A-Za-z]*";
        
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
                LocationName = GetLocationName(source.LocationName),
                Lat = source.Lat.Value,
                Long = source.Long.Value,
                ApprenticesCount = source.ApprenticesCount
            };
        }

        private static string GetLocationName(string locationName)
        {
            var separator = "";
            if (Regex.IsMatch(locationName, PostcodeRegex))
            {
                separator = ",";
            }
            else if (Regex.IsMatch(locationName, OutcodeDistrictRegex))
            {
                separator = " ";
            }

            if (string.IsNullOrEmpty(separator))
            {
                return locationName;
            }
            
            var locationPart = locationName.Substring(locationName.IndexOf(separator) + 1);
            return $"{locationPart.Trim()}, {locationName.Split(separator)[0].Trim()}";

        }
    }
}