using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.ProviderInterestRepository
{
    public class WhenAddingProviderInterest
    {
        private Mock<IEmployerDemandDataContext> _employerDemandDataContext;
        private Data.Repository.ProviderInterestRepository _providerInterestRepository;
        private ProviderInterest _providerInterest;

        [SetUp]
        public void Arrange()
        {
            _providerInterest = 
                new ProviderInterest
                {
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now
                };

            _employerDemandDataContext = new Mock<IEmployerDemandDataContext>();
            _employerDemandDataContext
                .Setup(x => x.ProviderInterests)
                .ReturnsDbSet(new List<ProviderInterest>());
            _providerInterestRepository = new Data.Repository.ProviderInterestRepository(
                Mock.Of<ILogger<Data.Repository.ProviderInterestRepository>>(), 
                _employerDemandDataContext.Object);
        }

        [Test]
        public async Task Then_The_CourseDemand_Item_Is_Added()
        {
            //Act
            var actual = await _providerInterestRepository.Insert(_providerInterest);
            
            //Assert
            _employerDemandDataContext.Verify(x=>
                x.ProviderInterests.AddAsync(_providerInterest, It.IsAny<CancellationToken>()), 
                Times.Once);
            _employerDemandDataContext.Verify(x=>
                x.SaveChanges(), 
                Times.Once);
            actual.Should().BeTrue();
        }

        [Test]
        public async Task Then_If_The_Is_A_Constraint_Exception_It_Is_Handled()
        {
            //Arrange
            _employerDemandDataContext.Setup(x => x.SaveChanges()).Throws(new DbUpdateException());
            
            //Act
            var actual = await _providerInterestRepository.Insert(_providerInterest);
            
            //Assert
            _employerDemandDataContext.Verify(x=>
                x.SaveChanges(), 
                Times.Once);
            actual.Should().BeFalse();
        }
    }
}