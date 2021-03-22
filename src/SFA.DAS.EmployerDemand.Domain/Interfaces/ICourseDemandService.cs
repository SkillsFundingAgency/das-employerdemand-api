using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandService
    {
        Task CreateDemand(Domain.Models.CourseDemand courseDemand);
    }
}