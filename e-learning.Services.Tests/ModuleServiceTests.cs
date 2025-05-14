using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace e_learning.Services.Tests
{
    public class ModuleServiceTests
    {
        private readonly Mock<IVideoRepository> _videoRepositoryMock;
        private readonly Mock<IModuleRepository> _moduleRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IOptions<CloudinarySettings>> _cloudinaryConfigMock;
        private readonly ModuleService _moduleService;

        public ModuleServiceTests()
        {
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _moduleRepositoryMock = new Mock<IModuleRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _cloudinaryConfigMock = new Mock<IOptions<CloudinarySettings>>();

            _cloudinaryConfigMock.Setup(x => x.Value).Returns(new CloudinarySettings
            {
                CloudName = "test-cloud",
                ApiKey = "test-key",
                ApiSecret = "test-secret"
            });

            _moduleService = new ModuleService(
                _httpContextAccessorMock.Object,
                _videoRepositoryMock.Object,
                _moduleRepositoryMock.Object,
                _cloudinaryConfigMock.Object
            );
        }

        [Fact]
        public async Task AddModuleAsync_ValidModule_ReturnsSuccess()
        {
            // Arrange
            var module = new Module
            {
                Title = "Test Module",
                CourseId = 1
            };

            // Act
            var result = await _moduleService.AddModuleAsync(module);

            // Assert
            Assert.Equal("Success", result);
            _moduleRepositoryMock.Verify(x => x.AddAsync(module), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingModule_ReturnsDeleted()
        {
            // Arrange
            var moduleId = 1;
            _moduleRepositoryMock.Setup(x => x.ExistsAsync(moduleId))
                .ReturnsAsync(true);

            // Act
            var result = await _moduleService.DeleteAsync(moduleId);

            // Assert
            Assert.Equal("Deleted", result);
            _moduleRepositoryMock.Verify(x => x.DeleteAsync(moduleId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingModule_ReturnsNotFound()
        {
            // Arrange
            var moduleId = 1;
            _moduleRepositoryMock.Setup(x => x.ExistsAsync(moduleId))
                .ReturnsAsync(false);

            // Act
            var result = await _moduleService.DeleteAsync(moduleId);

            // Assert
            Assert.Equal("NotFound", result);
            _moduleRepositoryMock.Verify(x => x.DeleteAsync(moduleId), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ExistingModule_ReturnsUpdated()
        {
            // Arrange
            var module = new Module
            {
                Id = 1,
                Title = "Updated Module",

            };

            _moduleRepositoryMock.Setup(x => x.ExistsAsync(module.Id))
                .ReturnsAsync(true);

            // Act
            var result = await _moduleService.UpdateAsync(module);

            // Assert
            Assert.Equal("Updated", result);
            _moduleRepositoryMock.Verify(x => x.UpdateAsync(module), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingModule_ReturnsNotFound()
        {
            // Arrange
            var module = new Module
            {
                Id = 1,
                Title = "Updated Module"
            };

            _moduleRepositoryMock.Setup(x => x.ExistsAsync(module.Id))
                .ReturnsAsync(false);

            // Act
            var result = await _moduleService.UpdateAsync(module);

            // Assert
            Assert.Equal("NotFound", result);
            _moduleRepositoryMock.Verify(x => x.UpdateAsync(module), Times.Never);
        }

        [Fact]
        public async Task GetModuleByIdAsync_ExistingModule_ReturnsModule()
        {
            // Arrange
            var moduleId = 1;
            var module = new Module
            {
                Id = moduleId,
                Title = "Test Module"
            };

            _moduleRepositoryMock.Setup(x => x.GetByIdAsync(moduleId))
                .ReturnsAsync(module);

            // Act
            var result = await _moduleService.GetModuleByIdAsync(moduleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(moduleId, result.Id);
            Assert.Equal(module.Title, result.Title);
        }

        [Fact]
        public async Task GetModuleByIdAsync_NonExistingModule_ReturnsNull()
        {
            // Arrange
            var moduleId = 1;
            _moduleRepositoryMock.Setup(x => x.GetByIdAsync(moduleId))
                .ReturnsAsync((Module)null);

            // Act
            var result = await _moduleService.GetModuleByIdAsync(moduleId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByCourseIdAsync_ExistingCourse_ReturnsModules()
        {
            // Arrange
            var courseId = 1;
            var modules = new List<Module>
            {
                new Module { Id = 1, Title = "Module 1", CourseId = courseId },
                new Module { Id = 2, Title = "Module 2", CourseId = courseId }
            };

            _moduleRepositoryMock.Setup(x => x.GetByCourseIdAsync(courseId))
                .ReturnsAsync(modules);

            // Act
            var result = await _moduleService.GetByCourseIdAsync(courseId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, m => Assert.Equal(courseId, m.CourseId));
        }

        [Fact]
        public async Task DeleteVideoFromModuleAsync_ExistingVideo_DeletesSuccessfully()
        {
            // Arrange
            var videoId = 1;
            var video = new e_learning.Data.Entities.Video
            {
                Id = videoId,
                PublicId = "test-public-id",
                Title = "Test Video"
            };

            _videoRepositoryMock.Setup(x => x.GetVideoByIdAsync(videoId))
                .ReturnsAsync(video);

            // Act
            var result = await _moduleService.DeleteVideoFromModuleAsync(videoId);

            // Assert
            Assert.Equal("Video deleted successfully", result);
            _videoRepositoryMock.Verify(x => x.DeleteVideoAsync(video), Times.Once);
        }

        [Fact]
        public async Task DeleteVideoFromModuleAsync_NonExistingVideo_ReturnsNotFound()
        {
            // Arrange
            var videoId = 1;
            _videoRepositoryMock.Setup(x => x.GetVideoByIdAsync(videoId))
                .ReturnsAsync((e_learning.Data.Entities.Video)null);

            // Act
            var result = await _moduleService.DeleteVideoFromModuleAsync(videoId);

            // Assert
            Assert.Equal("Video not found", result);
            _videoRepositoryMock.Verify(x => x.DeleteVideoAsync(It.IsAny<e_learning.Data.Entities.Video>()), Times.Never);
        }
    }
}