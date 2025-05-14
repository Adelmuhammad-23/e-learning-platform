using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
using Moq;

namespace e_learning.Services.Tests
{
    public class AdminServicesTests
    {
        private readonly Mock<IInstructorService> _instructorServiceMock;
        private readonly Mock<IAdminRepository> _adminRepositoryMock;
        private readonly AdminServices _adminServices;

        public AdminServicesTests()
        {
            _instructorServiceMock = new Mock<IInstructorService>();
            _adminRepositoryMock = new Mock<IAdminRepository>();
            _adminServices = new AdminServices(_instructorServiceMock.Object, _adminRepositoryMock.Object);
        }

        [Fact]
        public async Task Approved_ValidInstructor_ReturnsTrue()
        {
            // Arrange
            var instructor = new Instructor
            {

                Id = 1,
                Name = "Adel",
                Bio = "Software Engineer",
                Email = "adel@gmail.com"
            };

            _adminRepositoryMock.Setup(x => x.Approved(instructor))
                .ReturnsAsync(true);

            // Act
            var result = await _adminServices.Approved(instructor);

            // Assert
            Assert.True(result);
            _adminRepositoryMock.Verify(x => x.Approved(instructor), Times.Once);
        }

        [Fact]
        public async Task Approved_InvalidInstructor_ReturnsFalse()
        {
            // Arrange
            var instructor = new Instructor
            {

                Id = 1,
                Name = "Adel",
                Bio = "Software Engineer",
                Email = "adel@gmail.com"
            };

            _adminRepositoryMock.Setup(x => x.Approved(instructor))
                .ReturnsAsync(false);

            // Act
            var result = await _adminServices.Approved(instructor);

            // Assert
            Assert.False(result);
            _adminRepositoryMock.Verify(x => x.Approved(instructor), Times.Once);
        }

        [Fact]
        public async Task Approved_NullInstructor_ReturnsFalse()
        {
            // Arrange
            Instructor instructor = null;

            _adminRepositoryMock.Setup(x => x.Approved(instructor))
                .ReturnsAsync(false);

            // Act
            var result = await _adminServices.Approved(instructor);

            // Assert
            Assert.False(result);
            _adminRepositoryMock.Verify(x => x.Approved(instructor), Times.Once);
        }
    }
}