using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Implementations;
using Moq;

namespace e_learning.Services.Tests
{
    public class ChoiceServiceTests
    {
        private readonly Mock<IChoiceRepository> _choiceRepositoryMock;
        private readonly ChoiceService _choiceService;

        public ChoiceServiceTests()
        {
            _choiceRepositoryMock = new Mock<IChoiceRepository>();
            _choiceService = new ChoiceService(_choiceRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllChoices()
        {
            // Arrange
            var choices = new List<Choice>
            {
                new Choice { Id = 1, Text = "Choice 1" },
                new Choice { Id = 2, Text = "Choice 2" }
            };

            _choiceRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(choices);

            // Act
            var result = await _choiceService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(choices, result);
            _choiceRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingChoice_ReturnsChoice()
        {
            // Arrange
            var choiceId = 1;
            var choice = new Choice { Id = choiceId, Text = "Choice 1" };

            _choiceRepositoryMock.Setup(x => x.GetByIdAsync(choiceId))
                .ReturnsAsync(choice);

            // Act
            var result = await _choiceService.GetByIdAsync(choiceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(choiceId, result.Id);
            Assert.Equal(choice.Text, result.Text);
            _choiceRepositoryMock.Verify(x => x.GetByIdAsync(choiceId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingChoice_ReturnsNull()
        {
            // Arrange
            var choiceId = 1;

            _choiceRepositoryMock.Setup(x => x.GetByIdAsync(choiceId))
                .ReturnsAsync((Choice)null);

            // Act
            var result = await _choiceService.GetByIdAsync(choiceId);

            // Assert
            Assert.Null(result);
            _choiceRepositoryMock.Verify(x => x.GetByIdAsync(choiceId), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ValidChoice_AddsChoice()
        {
            // Arrange
            var choice = new Choice { Text = "New Choice" };

            // Act
            await _choiceService.AddAsync(choice);

            // Assert
            _choiceRepositoryMock.Verify(x => x.AddAsync(choice), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidChoice_UpdatesChoice()
        {
            // Arrange
            var choice = new Choice { Id = 1, Text = "Updated Choice" };

            // Act
            await _choiceService.UpdateAsync(choice);

            // Assert
            _choiceRepositoryMock.Verify(x => x.UpdateAsync(choice), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_DeletesChoice()
        {
            // Arrange
            var choiceId = 1;

            // Act
            await _choiceService.DeleteAsync(choiceId);

            // Assert
            _choiceRepositoryMock.Verify(x => x.DeleteAsync(choiceId), Times.Once);
        }
    }
}