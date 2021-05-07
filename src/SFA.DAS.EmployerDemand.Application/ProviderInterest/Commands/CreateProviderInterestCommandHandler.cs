using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommandHandler : IRequestHandler<CreateProviderInterestsCommand, CreateProviderInterestsCommandResult> 
    {
        private readonly IProviderInterestService _service;
        private readonly IValidator<CreateProviderInterestsCommand> _validator;

        public CreateProviderInterestCommandHandler(
            IProviderInterestService service, 
            IValidator<CreateProviderInterestsCommand> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<CreateProviderInterestsCommandResult> Handle(CreateProviderInterestsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }
            
            var result = await _service.CreateInterests(request.ProviderInterests);
            return new CreateProviderInterestsCommandResult
            {
                Ukprn = request.ProviderInterests.Ukprn,
                IsCreated = result
            };
        }
    }
}