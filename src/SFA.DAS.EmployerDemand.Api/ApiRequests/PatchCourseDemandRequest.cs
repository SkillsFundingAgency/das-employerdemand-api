namespace SFA.DAS.EmployerDemand.Api.ApiRequests
{
    public class PatchCourseDemandRequest
    {
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public bool? Stopped { get; set; }
    }
}