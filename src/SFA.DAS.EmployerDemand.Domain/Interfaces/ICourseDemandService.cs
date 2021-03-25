using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandService
    {
        Task<bool> CreateDemand(Models.CourseDemand courseDemand);
    }
}