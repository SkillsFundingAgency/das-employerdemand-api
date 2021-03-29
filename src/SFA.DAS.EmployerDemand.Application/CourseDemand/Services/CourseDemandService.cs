using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;

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

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandSummaryList()
        {
            var summaries = await _repository.GetAggregatedCourseDemandSummaryList();

            return summaries.Select(summary => (AggregatedCourseDemandSummary) summary);
        }
    }
}
