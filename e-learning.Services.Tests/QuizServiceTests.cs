using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace e_learning.Services.Tests
{
    public class QuizServiceTests
    {
        private readonly Mock<IQuizRepository> _quizRepositoryMock;
        private readonly Mock<ICourseServices> _courseServicesMock;
        private readonly Mock<IModuleService> _moduleServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEnrollmentService> _enrollmentServiceMock;
        private readonly Mock<IInstructorService> _instructorServiceMock;
        private readonly QuizService _quizService;
        private readonly HttpContext _httpContext;

        public QuizServiceTests()
        {
            _quizRepositoryMock = new Mock<IQuizRepository>();
            _courseServicesMock = new Mock<ICourseServices>();
            _moduleServiceMock = new Mock<IModuleService>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _enrollmentServiceMock = new Mock<IEnrollmentService>();
            _instructorServiceMock = new Mock<IInstructorService>();

            _httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(_httpContext);

            _quizService = new QuizService(
                _enrollmentServiceMock.Object,
                _httpContextAccessorMock.Object,
                _quizRepositoryMock.Object,
                _moduleServiceMock.Object,
                _courseServicesMock.Object,
                _instructorServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task SubmitQuizScore_NewEntry_AddsNewScore()
        {
            // Arrange
            var studentId = 1;
            var quizId = 1;
            var score = 85.5;

            _quizRepositoryMock.Setup(x => x.GetStudentQuizAsync(studentId, quizId))
                .ReturnsAsync((StudentQuiz)null);

            // Act
            await _quizService.SubmitQuizScoreAsync(studentId, quizId, score);

            // Assert
            _quizRepositoryMock.Verify(x => x.AddStudentQuizAsync(
                It.Is<StudentQuiz>(sq =>
                    sq.StudentId == studentId &&
                    sq.QuizId == quizId &&
                    sq.Score == score)),
                Times.Once);
            _quizRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SubmitQuizScore_ExistingEntry_UpdatesScore()
        {
            // Arrange
            var studentId = 1;
            var quizId = 1;
            var newScore = 90.0;
            var existingEntry = new StudentQuiz
            {
                StudentId = studentId,
                QuizId = quizId,
                Score = 85.0
            };

            _quizRepositoryMock.Setup(x => x.GetStudentQuizAsync(studentId, quizId))
                .ReturnsAsync(existingEntry);

            // Act
            await _quizService.SubmitQuizScoreAsync(studentId, quizId, newScore);

            // Assert
            Assert.Equal(newScore, existingEntry.Score);
            _quizRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_StudentEnrolled_ReturnsQuizzesWithQuestions()
        {
            // Arrange
            var studentId = 1;
            var claims = new List<Claim> { new Claim("studentId", studentId.ToString()) };
            _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var quizzes = new List<Quiz>
            {
                new Quiz { Id = 1, Title = "Quiz 1", CourseId = 1 },
                new Quiz { Id = 2, Title = "Quiz 2", CourseId = 2 }
            };

            var quizDtos = new List<CreateQuizDto>
            {
                new CreateQuizDto { Title = "Quiz 1", CourseId = 1, Questions = new List<CreateQuestionDto> { new CreateQuestionDto() } },
                new CreateQuizDto { Title = "Quiz 2", CourseId = 2, Questions = new List<CreateQuestionDto> { new CreateQuestionDto() } }
            };

            _quizRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(quizzes);
            _mapperMock.Setup(x => x.Map<List<CreateQuizDto>>(quizzes)).Returns(quizDtos);
            _enrollmentServiceMock.Setup(x => x.isEnrollment(studentId, It.IsAny<int>())).ReturnsAsync(true);
            _quizRepositoryMock.Setup(x => x.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync(new Quiz());
            _quizRepositoryMock.Setup(x => x.GetScore(studentId, It.IsAny<int>())).ReturnsAsync(85.0);

            // Act
            var result = await _quizService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, q =>
            {
                Assert.Equal("You can start solving the quiz.", q.Message);
                Assert.Equal(85.0, q.Score);
            });
        }

        [Fact]
        public async Task GetAllAsync_StudentNotEnrolled_ReturnsQuizzesWithoutQuestions()
        {
            // Arrange
            var studentId = 1;
            var claims = new List<Claim> { new Claim("studentId", studentId.ToString()) };
            _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var quizzes = new List<Quiz>
            {
                new Quiz { Id = 1, Title = "Quiz 1", CourseId = 1 }
            };

            var quizDtos = new List<CreateQuizDto>
            {
                new CreateQuizDto
                {
                    Title = "Quiz 1",
                    CourseId = 1,
                    Questions = new List<CreateQuestionDto> { new CreateQuestionDto() }
                }
            };

            _quizRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(quizzes);
            _mapperMock.Setup(x => x.Map<List<CreateQuizDto>>(quizzes)).Returns(quizDtos);
            _enrollmentServiceMock.Setup(x => x.isEnrollment(studentId, It.IsAny<int>())).ReturnsAsync(false);
            _quizRepositoryMock.Setup(x => x.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync(new Quiz());

            // Act
            var result = await _quizService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Empty(result[0].Questions);
            Assert.Equal("You are not registered in this course.", result[0].Message);
        }



        [Fact]
        public async Task DeleteAsync_ValidId_DeletesQuiz()
        {
            // Arrange
            var quizId = 1;

            // Act
            await _quizService.DeleteAsync(quizId);

            // Assert
            _quizRepositoryMock.Verify(x => x.DeleteAsync(quizId), Times.Once);
        }
    }
}