using e_learning.Data.Helpers;

namespace e_learning.infrastructure.Repositories
{
    public interface ICartRepository
    {
        Task<CartDto?> GetCartAsync(int studentId);
        Task SaveCartAsync(CartDto cart);
        Task DeleteCartAsync(int studentId);
    }

}
