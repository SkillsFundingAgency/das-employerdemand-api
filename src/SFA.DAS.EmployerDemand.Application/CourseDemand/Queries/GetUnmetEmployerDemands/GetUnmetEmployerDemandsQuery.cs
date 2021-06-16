using MediatR;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands
{
    public class GetUnmetEmployerDemandsQuery : IRequest<GetUnmetEmployerDemandsQueryResult>
    {
        public uint AgeOfDemandInDays { get ; set ; }
        public int? CourseId { get; set; }
    }
}