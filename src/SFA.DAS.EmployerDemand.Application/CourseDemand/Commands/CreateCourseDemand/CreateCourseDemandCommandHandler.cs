using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand
{
    public class CreateCourseDemandCommandHandler : IRequestHandler<CreateCourseDemandCommand, CreateCourseDemandCommandResponse>
    {
        private readonly ICourseDemandService _service;
        private readonly IValidator<CreateCourseDemandCommand> _validator;

        public CreateCourseDemandCommandHandler (ICourseDemandService service, IValidator<CreateCourseDemandCommand> validator)
        {
            _service = service;
            _validator = validator;
        }
        public async Task<CreateCourseDemandCommandResponse> Handle(CreateCourseDemandCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }
            
            var result = await _service.CreateDemand(request.CourseDemand);
            return new CreateCourseDemandCommandResponse
            {
                Id = request.CourseDemand.Id,
                IsCreated = result
            };
        }
    }
}