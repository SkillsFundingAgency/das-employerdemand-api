using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenCreatingEmployerDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            Domain.Models.CourseDemand courseDemand, 
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            await service.CreateDemand(courseDemand);
            
            repository.Verify(x=>x.Insert(It.Is<Domain.Entities.CourseDemand>(
                c=>c.EmailVerified == false
                   && c.Id.Equals(courseDemand.Id)
                   && c.Lat.Equals(courseDemand.Location.Lat)
                   && c.Long.Equals(courseDemand.Location.Lon)
                   && c.LocationName.Equals(courseDemand.Location.Name)
                   && c.CourseId.Equals(courseDemand.Course.Id)
                   && c.CourseTitle.Equals(courseDemand.Course.Title)
                   && c.CourseLevel.Equals(courseDemand.Course.Level)
                   && c.OrganisationName.Equals(courseDemand.OrganisationName)
                   && c.ContactEmailAddress.Equals(courseDemand.ContactEmailAddress)
                   && c.NumberOfApprentices.Equals(courseDemand.NumberOfApprentices)
                )));
        }
    }
}