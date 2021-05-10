using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Validation;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestsCommandValidator : IValidator<CreateProviderInterestsCommand>
    {
        private readonly ICourseDemandService _courseDemandService;

        public CreateProviderInterestsCommandValidator(ICourseDemandService courseDemandService)
        {
            _courseDemandService = courseDemandService;
        }

        public async Task<ValidationResult> ValidateAsync(CreateProviderInterestsCommand item)
        {
            var result = new ValidationResult();

            if (item.ProviderInterests.EmployerDemandIds == null || !item.ProviderInterests.EmployerDemandIds.Any())
            {
                result.AddError(nameof(item.ProviderInterests.EmployerDemandIds));
            }
            else if (! await _courseDemandService.EmployerDemandsExist(item.ProviderInterests.EmployerDemandIds))
            {
                result.AddError(nameof(item.ProviderInterests.EmployerDemandIds));
            }

            if (item.ProviderInterests.Ukprn < 1)
            {
                result.AddError(nameof(item.ProviderInterests.Ukprn));
            }

            if (!string.IsNullOrEmpty(item.ProviderInterests.Email))
            {
                try
                {
                    var emailAddress = new MailAddress(item.ProviderInterests.Email);
                    if (!emailAddress.Address.Equals(item.ProviderInterests.Email, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.AddError(nameof(item.ProviderInterests.Email));
                    }
                }
                catch (FormatException)
                {
                    result.AddError(nameof(item.ProviderInterests.Email));
                }
            }
            

            return result;
        }
    }
}