﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.VerifyCourseDemandEmail
{
    public class VerifyCourseDemandEmailCommandHandler : IRequestHandler<VerifyCourseDemandEmailCommand, VerifyCourseDemandEmailCommandResponse>
    {
        private readonly ICourseDemandService _service;

        public VerifyCourseDemandEmailCommandHandler (ICourseDemandService service)
        {
            _service = service;
        }

        public async Task<VerifyCourseDemandEmailCommandResponse> Handle(VerifyCourseDemandEmailCommand request, CancellationToken cancellationToken)
        {
            await _service.CreateDemand(request.CourseDemand);
            return new VerifyCourseDemandEmailCommandResponse
            {
                Id = request.CourseDemand.Id
            };
        }
    }
}
