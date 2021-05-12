using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;
using FluentAssertions;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenUpdatingEmployerCourseDemandEmailVerification
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Exists_Then_It_Is_Updated(
            Guid id,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            courseDemandEntity.Id = id;
            courseDemandEntity.EmailVerified = false;
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync(courseDemandEntity);
            var actual = await repository.VerifyCourseDemandEmail(id);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
            actual.Should().Be(courseDemandEntity.Id);
            courseDemandEntity.EmailVerified.Should().BeTrue();
        }
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Does_Not_Exist_Then_Null_Is_Returned(
            Guid id,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync((CourseDemand)null);
            var actual = await repository.VerifyCourseDemandEmail(id);
            mockDbContext.Verify(x => x.CourseDemands.Update(It.IsAny<CourseDemand>()), Times.Never);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Never);
            actual.Should().BeNull();
        }
    }
}
