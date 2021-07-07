namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class PatchCourseDemand : CourseDemandBase
    {
        public static implicit operator PatchCourseDemand(CourseDemand source)
        {
            return new PatchCourseDemand
            {
                Stopped = source.Stopped,
                EmailVerified = source.EmailVerified,
                OrganisationName = source.OrganisationName,
                ContactEmailAddress = source.ContactEmailAddress,
                NumberOfApprentices = source.NumberOfApprentices
            };
        }
    }
}