using System;
using System.Net.Mail;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Validation;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommandValidator : IValidator<CreateProviderInterestCommand>
    {
        public Task<ValidationResult> ValidateAsync(CreateProviderInterestCommand item)
        {
            var result = new ValidationResult();

            if (item.ProviderInterest.Id == Guid.Empty)
            {
                result.AddError(nameof(item.ProviderInterest.Id));
            }

            if (item.ProviderInterest.EmployerDemandId == Guid.Empty)
            {
                result.AddError(nameof(item.ProviderInterest.EmployerDemandId));
            }

            if (item.ProviderInterest.Ukprn < 1)
            {
                result.AddError(nameof(item.ProviderInterest.Ukprn));
            }

            if (!string.IsNullOrEmpty(item.ProviderInterest.Email))
            {
                try
                {
                    var emailAddress = new MailAddress(item.ProviderInterest.Email);
                    if (!emailAddress.Address.Equals(item.ProviderInterest.Email, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.AddError(nameof(item.ProviderInterest.Email));
                    }
                }
                catch (FormatException)
                {
                    result.AddError(nameof(item.ProviderInterest.Email));
                }
            }
            

            return Task.FromResult(result);
        }
    }
}