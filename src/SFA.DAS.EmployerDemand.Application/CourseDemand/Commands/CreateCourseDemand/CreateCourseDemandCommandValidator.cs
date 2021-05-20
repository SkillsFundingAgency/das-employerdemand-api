using System;
using System.Net.Mail;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Validation;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand
{
    public class CreateCourseDemandCommandValidator : IValidator<CreateCourseDemandCommand>
    {
        public Task<ValidationResult> ValidateAsync(CreateCourseDemandCommand item)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(item.CourseDemand.OrganisationName))
            {
                result.AddError(nameof(item.CourseDemand.OrganisationName));
            }
            if (string.IsNullOrEmpty(item.CourseDemand.ContactEmailAddress))
            {
                result.AddError(nameof(item.CourseDemand.ContactEmailAddress));
            }
            else
            {
                try
                {
                    var emailAddress = new MailAddress(item.CourseDemand.ContactEmailAddress);
                    if (!emailAddress.Address.Equals(item.CourseDemand.ContactEmailAddress, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.AddError(nameof(item.CourseDemand.ContactEmailAddress));
                    }
                }
                catch (FormatException)
                {
                    result.AddError(nameof(item.CourseDemand.ContactEmailAddress));
                }
            }

            if (item.CourseDemand.Course.Id == 0 || item.CourseDemand.Course.Level == 0 || string.IsNullOrEmpty(item.CourseDemand.Course.Title) || string.IsNullOrEmpty(item.CourseDemand.Course.Route))
            {
                result.AddError(nameof(item.CourseDemand.Course));
            }

            if (item.CourseDemand.Location.Lat == 0 || item.CourseDemand.Location.Lon == 0 ||
                string.IsNullOrEmpty(item.CourseDemand.Location.Name))
            {
                result.AddError(nameof(item.CourseDemand.Location));
            }

            return Task.FromResult(result);
        }
    }
}