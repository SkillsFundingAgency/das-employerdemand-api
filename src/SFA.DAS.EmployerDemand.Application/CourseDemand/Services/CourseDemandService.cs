using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Services
{
    public class CourseDemandService : ICourseDemandService
    {
        private readonly ICourseDemandRepository _repository;

        public CourseDemandService (ICourseDemandRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CreateDemand(Domain.Models.CourseDemand courseDemand)
        {
            return await _repository.Insert(courseDemand);
        }
    }
}