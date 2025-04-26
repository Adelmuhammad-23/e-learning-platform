using e_learning.Data.Helpers;

namespace e_learning.Services.Abstructs
{
    public interface ICartService
    {
        public Task<CartDto> AddToCartAsync(int studentId, int courseId);
        Task<CartDto?> GetCartAsync(int studentId);
        Task RemoveFromCartAsync(int studentId, int courseId);
    }

}
