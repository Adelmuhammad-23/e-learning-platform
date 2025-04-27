using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public CartService(ICartRepository cartRepository, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _cartRepository = cartRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
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