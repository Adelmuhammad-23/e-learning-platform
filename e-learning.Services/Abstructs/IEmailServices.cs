namespace e_learning.Services.Abstructs
{
    public interface IEmailServices
    {
        public Task<string> SendEmailAsync(string email, string massage, string? reason);

    }
}
