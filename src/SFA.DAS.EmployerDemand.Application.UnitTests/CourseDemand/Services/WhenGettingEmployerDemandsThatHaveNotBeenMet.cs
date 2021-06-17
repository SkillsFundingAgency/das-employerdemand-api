using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenGettingEmployerDemandsThatHaveNotBeenMet
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Ids_Returned_Of_Demands_Not_Met_After_Inputted_Days(
            uint ageOfDemand,
            int courseId,
            List<Domain.Entities.CourseDemand> demands,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetCourseDemandsWithNoProviderInterest(ageOfDemand, courseId))
                .ReturnsAsync(demands);
            
            //Act
            var actual = await service.GetUnmetEmployerDemands(ageOfDemand);
            
            //Assert
            actual.Should().BeEquivalentTo(demands.Select(c=>(Domain.Models.CourseDemand)c).ToList());
        }
    }
}