namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class Course
    {
        public int Id { get ; set ; }
        public string Title { get ; set ; }
        public int Level { get ; set ; }
        public string Route { get ; set ; }

        public static implicit operator Course(Domain.Models.Course source)
        {
            return new Course
            {
                Id = source.Id,
                Title = source.Title,
                Level = source.Level,
                Route = source.Route
            };
        }
    }
}