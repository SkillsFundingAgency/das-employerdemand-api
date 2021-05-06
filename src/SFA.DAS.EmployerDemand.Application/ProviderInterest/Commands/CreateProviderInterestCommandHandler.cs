using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommandHandler : IRequestHandler<CreateProviderInterestCommand, CreateProviderInterestCommandResult> 
    {
        private readonly IProviderInterestService _service;
        private readonly IValidator<CreateProviderInterestCommand> _validator;

        public CreateProviderInterestCommandHandler(
            IProviderInterestService service, 
            IValidator<CreateProviderInterestCommand> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<CreateProviderInterestCommandResult> Handle(CreateProviderInterestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }
            
            var result = await _service.CreateInterest(request.ProviderInterest);
            return new CreateProviderInterestCommandResult
            {
                Id = request.ProviderInterest.Id,
                IsCreated = result
            };
        }
    }
}