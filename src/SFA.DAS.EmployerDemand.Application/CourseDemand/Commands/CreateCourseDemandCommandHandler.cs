using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommandHandler : IRequestHandler<CreateCourseDemandCommand, Guid>
    {
        private readonly ICourseDemandService _service;
        private readonly IValidator<CreateCourseDemandCommand> _validator;

        public CreateCourseDemandCommandHandler (ICourseDemandService service, IValidator<CreateCourseDemandCommand> validator)
        {
            _service = service;
            _validator = validator;
        }
        public async Task<Guid> Handle(CreateCourseDemandCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }
            
            await _service.CreateDemand(request.CourseDemand);
            
            return request.CourseDemand.Id;
        }
    }
}