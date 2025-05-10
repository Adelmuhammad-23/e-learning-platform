using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using StackExchange.Redis;
using System.Text.Json;

public class CartRepository : ICartRepository
{
    private readonly IDatabase _database;

    public CartRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    private string GetCartKey(int studentId) => $"cart:student:{studentId}";

    public async Task<CartDto?> GetCartAsync(int studentId)
    {
        var data = await _database.StringGetAsync(GetCartKey(studentId));
        if (data.IsNullOrEmpty)
            return null;

        var cart = JsonSerializer.Deserialize<CartDto>(data);

        if (cart != null && cart.CartId == Guid.Empty)
        {
            cart.CartId = Guid.NewGuid();
            await SaveCartAsync(cart);
        }

        return cart;
    }

    public async Task SaveCartAsync(CartDto cart)
    {
        if (cart.CartId == Guid.Empty)
        {
            cart.CartId = Guid.NewGuid();
        }

        var data = JsonSerializer.Serialize(cart);
        await _database.StringSetAsync(GetCartKey(cart.StudentId), data, TimeSpan.FromDays(1));
    }
    public async Task<CartDto> GetCartByIdAsync(Guid cartId)
    {
        // استرجاع الكارت من Redis باستخدام cartId
        var cartData = await _database.StringGetAsync(cartId.ToString());
        if (string.IsNullOrEmpty(cartData))
            return null;

        // تحويل الـ JSON المخزن في Redis إلى كائن CartDto
        return Newtonsoft.Json.JsonConvert.DeserializeObject<CartDto>(cartData);
    }

    public async Task DeleteCartAsync(int studentId)
    {
        await _database.KeyDeleteAsync(GetCartKey(studentId));
    }
}
