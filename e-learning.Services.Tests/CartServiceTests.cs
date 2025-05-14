
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
using Moq;

namespace e_learning.Services.Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly Mock<PayPalService> _payPalServiceMock;
        private readonly Mock<IEnrollmentService> _enrollmentServiceMock;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _courseRepositoryMock = new Mock<ICourseRepository>();
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _payPalServiceMock = new Mock<PayPalService>();
            _enrollmentServiceMock = new Mock<IEnrollmentService>();

            _cartService = new CartService(
                _cartRepositoryMock.Object,
                _studentRepositoryMock.Object,
                _courseRepositoryMock.Object,
                _payPalServiceMock.Object,
                _enrollmentServiceMock.Object
            );
        }

        [Fact]
        public async Task CheckoutAsync_EmptyCart_ThrowsException()
        {
            // Arrange
            var studentId = 1;
            _cartRepositoryMock.Setup(x => x.GetCartAsync(studentId))
                .ReturnsAsync((CartDto)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _cartService.CheckoutAsync(studentId));
        }

        [Fact]
        public async Task CheckoutAsync_ValidCart_ReturnsApprovalUrl()
        {
            // Arrange
            var studentId = 1;
            var cart = new CartDto
            {
                StudentId = studentId,
                CartId = Guid.NewGuid(),
                Courses = new List<CartItemDto>
                {
                    new CartItemDto { CourseId = 1, Price = 100 },
                    new CartItemDto { CourseId = 2, Price = 200 }
                }
            };

            var expectedApprovalUrl = "https://paypal.com/approval";

            _cartRepositoryMock.Setup(x => x.GetCartAsync(studentId))
                .ReturnsAsync(cart);
            _payPalServiceMock.Setup(x => x.CreateOrderAsync(300, studentId))
                .ReturnsAsync(expectedApprovalUrl);

            // Act
            var result = await _cartService.CheckoutAsync(studentId);

            // Assert
            Assert.Equal(expectedApprovalUrl, result);
        }



        [Fact]
        public async Task RemoveFromCartAsync_ExistingItem_RemovesItem()
        {
            // Arrange
            var studentId = 1;
            var courseId = 1;
            var cart = new CartDto
            {
                StudentId = studentId,
                CartId = Guid.NewGuid(),
                Courses = new List<CartItemDto>
                {
                    new CartItemDto { CourseId = courseId, Price = 100 }
                }
            };

            _cartRepositoryMock.Setup(x => x.GetCartAsync(studentId))
                .ReturnsAsync(cart);

            // Act
            await _cartService.RemoveFromCartAsync(studentId, courseId);

            // Assert
            _cartRepositoryMock.Verify(x => x.SaveCartAsync(It.Is<CartDto>(c => !c.Courses.Any())), Times.Once);
        }

        [Fact]
        public async Task GetCartAsync_ExistingCart_ReturnsCart()
        {
            // Arrange
            var studentId = 1;
            var cart = new CartDto
            {
                StudentId = studentId,
                CartId = Guid.NewGuid(),
                Courses = new List<CartItemDto>()
            };

            _cartRepositoryMock.Setup(x => x.GetCartAsync(studentId))
                .ReturnsAsync(cart);

            // Act
            var result = await _cartService.GetCartAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cart.StudentId, result.StudentId);
            Assert.Equal(cart.CartId, result.CartId);
        }

        [Fact]
        public async Task CheckoutByCartIdAsync_ValidCart_EnrollsAndReturnsApprovalUrl()
        {
            // Arrange
            var studentId = 1;
            var cartId = Guid.NewGuid();
            var cart = new CartDto
            {
                StudentId = studentId,
                CartId = cartId,
                Courses = new List<CartItemDto>
                {
                    new CartItemDto { CourseId = 1, Price = 100 },
                    new CartItemDto { CourseId = 2, Price = 200 }
                }
            };

            var expectedApprovalUrl = "https://paypal.com/approval";

            _cartRepositoryMock.Setup(x => x.GetCartByIdAsync(cartId))
                .ReturnsAsync(cart);
            _payPalServiceMock.Setup(x => x.CreateOrderAsync(300, studentId))
                .ReturnsAsync(expectedApprovalUrl);

            // Act
            var result = await _cartService.CheckoutByCartIdAsync(studentId, cartId);

            // Assert
            Assert.Equal(expectedApprovalUrl, result);
            _enrollmentServiceMock.Verify(x => x.EnrollStudentInCourseAsync(studentId, It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task CheckoutByCartIdAsync_InvalidCart_ThrowsException()
        {
            // Arrange
            var studentId = 1;
            var cartId = Guid.NewGuid();

            _cartRepositoryMock.Setup(x => x.GetCartByIdAsync(cartId))
                .ReturnsAsync((CartDto)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _cartService.CheckoutByCartIdAsync(studentId, cartId));
        }
    }
}