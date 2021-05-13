using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.VerifyCourseDemandEmail_Commands
{
    public class VerifyCourseDemandEmailHandler : IRequestHandler<VerifyCourseDemandEmailCommand, VerifyCourseDemandEmailResponse>
    {
        private readonly ICourseDemandService _service;

        public VerifyCourseDemandEmailHandler (ICourseDemandService service)
        {
            _service = service;
        }

        public async Task<VerifyCourseDemandEmailResponse> Handle(VerifyCourseDemandEmailCommand request, CancellationToken cancellationToken)
        {
            await _service.CreateDemand(request.CourseDemand);
            return new VerifyCourseDemandEmailResponse
            {
                Id = request.CourseDemand.Id
            };
        }
    }
}
