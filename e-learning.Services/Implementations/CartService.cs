using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly PayPalService _payPalService;
        private readonly IEnrollmentService _enrollmentService;
        public CartService(ICartRepository cartRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, PayPalService payPalService, IEnrollmentService enrollmentService)
        {
            _cartRepository = cartRepository;
            _studentRepository = studentRepository;

            _courseRepository = courseRepository;
            _payPalService = payPalService;
            _enrollmentService = enrollmentService;
        }

        public async Task<string> CheckoutAsync(int studentId)
        {
            var cart = await _cartRepository.GetCartAsync(studentId);
            if (cart == null || !cart.Courses.Any())
                throw new Exception("Cart is empty");

            var total = cart.Courses.Sum(c => c.Price);
            var approvalUrl = await _payPalService.CreateOrderAsync(total, studentId);

            return approvalUrl;
        }

        public async Task<CartDto> AddToCartAsync(int studentId, int courseId)
        {

            var cart = await _cartRepository.GetCartAsync(studentId)
                ?? new CartDto
                {
                    StudentId = studentId,
                    CartId = Guid.NewGuid()
                };

            if (!cart.Courses.Any(x => x.CourseId == courseId))
            {
                var course = await _courseRepository.GetCourseByIdAsync(courseId);
                var newCartItem = new CartItemDto
                {

                    CourseId = course.Id,
                    CourseTitle = course.Title,
                    Price = course.Price

                };
                cart.Courses.Add(newCartItem);
                await _cartRepository.SaveCartAsync(cart);
            }

            return cart;
        }

        public async Task<CartDto?> GetCartAsync(int studentId)
        {
            return await _cartRepository.GetCartAsync(studentId);
        }
        public async Task<string> CheckoutByCartIdAsync(int studentId, Guid cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            if (cart == null || cart.StudentId != studentId)
                throw new Exception("Invalid cart");

            var total = cart.Courses.Sum(c => c.Price);

            foreach (var course in cart.Courses)
            {
                await _enrollmentService.EnrollStudentInCourseAsync(studentId, course.CourseId);
            }

            var approvalUrl = await _payPalService.CreateOrderAsync(total, studentId);
            return approvalUrl;
        }

        public async Task RemoveFromCartAsync(int studentId, int courseId)
        {
            var cart = await _cartRepository.GetCartAsync(studentId);
            if (cart == null) return;

            var item = cart.Courses.FirstOrDefault(x => x.CourseId == courseId);
            if (item != null)
            {
                cart.Courses.Remove(item);
                await _cartRepository.SaveCartAsync(cart);
            }
        }
    }
}